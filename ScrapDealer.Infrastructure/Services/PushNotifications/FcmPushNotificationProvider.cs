using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.Options;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.Services.PushNotifications;

public class FcmPushNotificationProvider(
    HttpClient httpClient,
    IOptions<PushNotificationSettings> settings,
    ILogger<FcmPushNotificationProvider> logger)
    : IPushNotificationProvider
{
    private const string FcmScope =
        "https://www.googleapis.com/auth/firebase.messaging";

    private const string FcmEndpointFormat =
        "https://fcm.googleapis.com/v1/projects/{0}/messages:send";

    private static readonly SemaphoreSlim SendGate = new(8);

    private readonly object _credentialLock = new();

    private GoogleCredential? _credential;

    public PushProvider Provider => PushProvider.Fcm;

    public async Task<IReadOnlyCollection<PushDeliveryOutcome>> SendAsync(
        string title,
        string body,
        IReadOnlyCollection<PushDevice> devices,
        CancellationToken cancellationToken)
    {
        if (devices.Count == 0)
            return Array.Empty<PushDeliveryOutcome>();

        var fcmSettings = settings.Value.Fcm;

        if (string.IsNullOrWhiteSpace(fcmSettings.ProjectId))
        {
            throw new InvalidOperationException(
                "FCM ProjectId is not configured.");
        }

        var credential = GetCredential();

        var tokenAccess = (ITokenAccess)credential;

        var accessToken = await GetAccessTokenAsync(tokenAccess, cancellationToken);

        var pendingDevices = devices.Select((device, index) => (device, index)).ToList();
        var outcomes = new List<PushDeliveryOutcome>(devices.Count);
        Exception? authorizationFailure = null;

        for (var attempt = 1; pendingDevices.Count > 0 && attempt <= 3; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var attemptResults = new ConcurrentBag<(int Index, PushDevice Device, DeviceSendResult Result)>();

            await Parallel.ForEachAsync(
                pendingDevices,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = 8
                },
                async (item, token) =>
                {
                    var existingAuthorizationFailure = Interlocked.CompareExchange(
                        ref authorizationFailure,
                        null,
                        null);
                    if (existingAuthorizationFailure is not null)
                    {
                        attemptResults.Add((
                            item.index,
                            item.device,
                            new DeviceSendResult(
                                PushDeliveryStatus.PermanentFailure,
                                existingAuthorizationFailure,
                                IsAuthorizationFailure: true)));
                        return;
                    }

                    await SendGate.WaitAsync(token);
                    try
                    {
                        var result = await SendOneAsync(
                            title,
                            body,
                            item.device.Token,
                            fcmSettings.ProjectId,
                            accessToken,
                            token);
                        if (result.IsAuthorizationFailure)
                        {
                            Interlocked.CompareExchange(
                                ref authorizationFailure,
                                result.Exception,
                                null);
                        }
                        attemptResults.Add((item.index, item.device, result));
                    }
                    finally
                    {
                        SendGate.Release();
                    }
                });

            var retryDevices = new List<(PushDevice device, int index)>();
            var fatalAuthorizationFailure = Interlocked.CompareExchange(
                ref authorizationFailure,
                null,
                null);
            foreach (var result in attemptResults.OrderBy(result => result.Index))
            {
                if (result.Result.Status == PushDeliveryStatus.TransientFailure && attempt < 3)
                {
                    if (fatalAuthorizationFailure is not null)
                    {
                        outcomes.Add(new PushDeliveryOutcome(
                            result.Device,
                            PushDeliveryStatus.PermanentFailure,
                            fatalAuthorizationFailure));
                        continue;
                    }

                    retryDevices.Add((result.Device, result.Index));
                    continue;
                }

                outcomes.Add(new PushDeliveryOutcome(
                    result.Device,
                    result.Result.Status,
                    result.Result.Exception));
            }

            pendingDevices = retryDevices;
            if (pendingDevices.Count > 0 && attempt < 3)
            {
                await DelayBeforeRetryAsync(attempt, cancellationToken);
            }
        }

        return outcomes;
    }

    private async Task<string> GetAccessTokenAsync(
        ITokenAccess tokenAccess,
        CancellationToken cancellationToken)
    {
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                return await tokenAccess.GetAccessTokenForRequestAsync(null, cancellationToken);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                if (attempt >= 3)
                    throw;

                logger.LogWarning(
                    "Timed out while acquiring an FCM access token on attempt {Attempt}.",
                    attempt);
                await DelayBeforeRetryAsync(attempt, cancellationToken);
            }
            catch (HttpRequestException exception)
            {
                if (attempt >= 3)
                    throw;

                logger.LogWarning(
                    exception,
                    "Network error while acquiring an FCM access token on attempt {Attempt}.",
                    attempt);
                await DelayBeforeRetryAsync(attempt, cancellationToken);
            }
            catch (Google.GoogleApiException exception)
                when (IsTransientStatusCode(exception.HttpStatusCode))
            {
                if (attempt >= 3)
                    throw;

                logger.LogWarning(
                    exception,
                    "FCM token service returned retryable status {StatusCode} on attempt {Attempt}.",
                    (int)exception.HttpStatusCode,
                    attempt);
                await DelayBeforeRetryAsync(attempt, cancellationToken);
            }
        }
    }

    private static Task DelayBeforeRetryAsync(int attempt, CancellationToken cancellationToken)
    {
        var baseDelayMs = 250 * Math.Pow(2, attempt - 1);
        var jitteredDelayMs = baseDelayMs + Random.Shared.NextDouble() * baseDelayMs * 0.5;
        return Task.Delay(TimeSpan.FromMilliseconds(jitteredDelayMs), cancellationToken);
    }

    private static bool IsTransientStatusCode(HttpStatusCode statusCode)
        => statusCode == HttpStatusCode.RequestTimeout ||
           (int)statusCode == 429 ||
           (int)statusCode >= 500;

    private async Task<DeviceSendResult> SendOneAsync(
        string title,
        string body,
        string token,
        string projectId,
        string accessToken,
        CancellationToken cancellationToken)
    {
        var payload = new
        {
            message = new
            {
                token,
                notification = new
                {
                    title,
                    body
                },
                android = new
                {
                    priority = "HIGH"
                }
            }
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            string.Format(FcmEndpointFormat, projectId));

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        try
        {
            using var response = await httpClient.SendAsync(
                request,
                cancellationToken);

            var responseBody = await response.Content.ReadAsStringAsync(
                cancellationToken);

            if (response.IsSuccessStatusCode)
                return new DeviceSendResult(PushDeliveryStatus.Accepted);

            var error = ParseFcmError(responseBody);

            logger.LogWarning(
                "FCM notification failed. StatusCode: {StatusCode}, " +
                "ErrorCode: {ErrorCode}, ErrorStatus: {ErrorStatus}, " +
                "FcmErrorCode: {FcmErrorCode}, ErrorMessage: {ErrorMessage}",
                (int)response.StatusCode,
                error.Code,
                error.Status,
                error.ErrorCode,
                error.Message);

            var status = ClassifyError(response.StatusCode, error);
            return new DeviceSendResult(
                status,
                new HttpRequestException(
                    $"FCM returned {(int)response.StatusCode} ({response.StatusCode}): {error.Message}"),
                response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden);
        }
        catch (OperationCanceledException)
            when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning(
                "FCM notification request timed out.");

            return new DeviceSendResult(
                PushDeliveryStatus.TransientFailure,
                new TimeoutException("FCM notification request timed out."));
        }
        catch (HttpRequestException exception)
        {
            logger.LogWarning(
                exception,
                "Network error occurred while sending FCM notification.");

            return new DeviceSendResult(PushDeliveryStatus.TransientFailure, exception);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Unexpected error occurred while sending an FCM notification.");
            return new DeviceSendResult(PushDeliveryStatus.PermanentFailure, exception);
        }
    }

    private static PushDeliveryStatus ClassifyError(
        HttpStatusCode statusCode,
        FcmError error)
    {
        // FCM explicitly reports that the registration token
        // is no longer valid.
        if (string.Equals(
                error.Status,
                "UNREGISTERED",
                StringComparison.OrdinalIgnoreCase))
        {
            return PushDeliveryStatus.InvalidToken;
        }

        if (string.Equals(
                error.ErrorCode,
                "UNREGISTERED",
                StringComparison.OrdinalIgnoreCase))
        {
            return PushDeliveryStatus.InvalidToken;
        }

        // Authentication / authorization / configuration problems.
        if (statusCode is
            HttpStatusCode.Unauthorized or
            HttpStatusCode.Forbidden)
        {
            return PushDeliveryStatus.PermanentFailure;
        }

        // Other 4xx responses indicate a rejected request.
        if (statusCode == HttpStatusCode.RequestTimeout ||
            (int)statusCode == 429 ||
            (int)statusCode >= 500)
        {
            return PushDeliveryStatus.TransientFailure;
        }

        return PushDeliveryStatus.PermanentFailure;
    }

    private static FcmError ParseFcmError(string responseBody)
    {
        if (string.IsNullOrWhiteSpace(responseBody))
            return FcmError.Empty;

        try
        {
            using var json = JsonDocument.Parse(responseBody);

            if (!json.RootElement.TryGetProperty(
                    "error",
                    out var errorElement))
            {
                return FcmError.Empty;
            }

            var code = errorElement.TryGetProperty(
                    "code",
                    out var codeElement)
                ? codeElement.ToString()
                : null;

            var status = errorElement.TryGetProperty(
                    "status",
                    out var statusElement)
                ? statusElement.GetString()
                : null;

            var message = errorElement.TryGetProperty(
                    "message",
                    out var messageElement)
                ? messageElement.GetString()
                : null;

            string? errorCode = null;

            /*
             * FCM HTTP v1 can return token-specific errors inside:
             *
             * error.details[].errorCode
             *
             * For example:
             *
             * "error": {
             *   "status": "NOT_FOUND",
             *   "details": [
             *     {
             *       "@type":
             *         "type.googleapis.com/google.firebase.fcm.v1.FcmError",
             *       "errorCode": "UNREGISTERED"
             *     }
             *   ]
             * }
             */
            if (errorElement.TryGetProperty(
                    "details",
                    out var detailsElement) &&
                detailsElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var detail in detailsElement.EnumerateArray())
                {
                    if (detail.ValueKind != JsonValueKind.Object)
                        continue;

                    if (!detail.TryGetProperty(
                            "errorCode",
                            out var errorCodeElement))
                    {
                        continue;
                    }

                    errorCode = errorCodeElement.GetString();

                    if (!string.IsNullOrWhiteSpace(errorCode))
                        break;
                }
            }

            return new FcmError(
                code,
                status,
                message,
                errorCode);
        }
        catch (JsonException)
        {
            return new FcmError(
                null,
                null,
                responseBody,
                null);
        }
    }

    private GoogleCredential GetCredential()
    {
        if (_credential is not null)
            return _credential;

        lock (_credentialLock)
        {
            if (_credential is not null)
                return _credential;

            var serviceAccountJsonPath =
                settings.Value.Fcm.ServiceAccountJsonPath;

            if (string.IsNullOrWhiteSpace(serviceAccountJsonPath))
            {
                throw new InvalidOperationException(
                    "FCM ServiceAccountJsonPath is not configured.");
            }

            var resolvedPath = Path.IsPathRooted(serviceAccountJsonPath)
                ? serviceAccountJsonPath
                : Path.Combine(
                    AppContext.BaseDirectory,
                    serviceAccountJsonPath);

            if (!File.Exists(resolvedPath))
            {
                throw new FileNotFoundException(
                    $"FCM service account JSON file was not found: {resolvedPath}",
                    resolvedPath);
            }

#pragma warning disable CS0618
            _credential = GoogleCredential
                .FromFile(resolvedPath)
                .CreateScoped(FcmScope);
#pragma warning restore CS0618

            return _credential;
        }
    }

    private sealed record FcmError(
        string? Code,
        string? Status,
        string? Message,
        string? ErrorCode)
    {
        public static FcmError Empty { get; } =
            new(null, null, null, null);
    }

    private sealed record DeviceSendResult(
        PushDeliveryStatus Status,
        Exception? Exception = null,
        bool IsAuthorizationFailure = false);
}
