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

    public async Task<IReadOnlyCollection<string>> SendAsync(
        string title,
        string body,
        IReadOnlyCollection<PushDevice> devices,
        CancellationToken cancellationToken)
    {
        if (devices.Count == 0)
            return Array.Empty<string>();

        var fcmSettings = settings.Value.Fcm;

        if (string.IsNullOrWhiteSpace(fcmSettings.ProjectId))
        {
            throw new InvalidOperationException(
                "FCM ProjectId is not configured.");
        }

        var credential = GetCredential();

        var tokenAccess = (ITokenAccess)credential;

        var accessToken = await tokenAccess.GetAccessTokenForRequestAsync(
            null,
            cancellationToken);

        var invalidTokens = new ConcurrentBag<string>();

        var tasks = devices.Select(async device =>
        {
            await SendGate.WaitAsync(cancellationToken);

            try
            {
                var outcome = await SendOneAsync(
                    title,
                    body,
                    device.Token,
                    fcmSettings.ProjectId,
                    accessToken,
                    cancellationToken);

                switch (outcome)
                {
                    case SendOutcome.TokenInvalid:
                        invalidTokens.Add(device.Token);
                        break;

                    case SendOutcome.TransientFailure:
                        throw new InvalidOperationException(
                            "FCM returned a transient failure while sending a notification.");

                    case SendOutcome.PermanentFailure:
                        throw new InvalidOperationException(
                            "FCM rejected the notification request.");

                    case SendOutcome.Success:
                        break;
                }
            }
            finally
            {
                SendGate.Release();
            }
        });

        await Task.WhenAll(tasks);

        return invalidTokens.ToArray();
    }

    private async Task<SendOutcome> SendOneAsync(
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
                return SendOutcome.Success;

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

            return ClassifyError(response.StatusCode, error);
        }
        catch (OperationCanceledException)
            when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning(
                "FCM notification request timed out.");

            return SendOutcome.TransientFailure;
        }
        catch (HttpRequestException exception)
        {
            logger.LogWarning(
                exception,
                "Network error occurred while sending FCM notification.");

            return SendOutcome.TransientFailure;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SendOutcome.TransientFailure;
        }
    }

    private static SendOutcome ClassifyError(
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
            return SendOutcome.TokenInvalid;
        }

        if (string.Equals(
                error.ErrorCode,
                "UNREGISTERED",
                StringComparison.OrdinalIgnoreCase))
        {
            return SendOutcome.TokenInvalid;
        }

        // Authentication / authorization / configuration problems.
        if (statusCode is
            HttpStatusCode.Unauthorized or
            HttpStatusCode.Forbidden)
        {
            return SendOutcome.PermanentFailure;
        }

        // Other 4xx responses indicate a rejected request.
        if ((int)statusCode >= 400 &&
            (int)statusCode < 500)
        {
            return SendOutcome.PermanentFailure;
        }

        // Server-side errors should normally be retried later.
        if ((int)statusCode >= 500)
            return SendOutcome.TransientFailure;

        return SendOutcome.TransientFailure;
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

    private enum SendOutcome
    {
        Success,
        TokenInvalid,
        TransientFailure,
        PermanentFailure
    }
}
