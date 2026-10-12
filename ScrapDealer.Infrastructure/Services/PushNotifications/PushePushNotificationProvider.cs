using Microsoft.Extensions.Options;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.Options;
using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.Services.PushNotifications
{
    public class PushePushNotificationProvider(HttpClient httpClient, IOptions<PushNotificationSettings> settings)
        : IPushNotificationProvider
    {
        private const int MaxTokensPerRequest = 500;
        private static readonly SemaphoreSlim SendGate = new(4);

        public PushProvider Provider => PushProvider.Pushe;

        public async Task<IReadOnlyCollection<PushDeliveryOutcome>> SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken)
        {
            var appToken = settings.Value.Pushe.AppToken ?? throw new InvalidOperationException("Pushe AppToken is not configured.");
            var appId = settings.Value.Pushe.AppId ?? throw new InvalidOperationException("Pushe AppId is not configured.");

            var batches = devices.Chunk(MaxTokensPerRequest).ToArray();
            var outcomes = new ConcurrentBag<PushDeliveryOutcome>();
            await Parallel.ForEachAsync(
                batches,
                new ParallelOptions { CancellationToken = cancellationToken, MaxDegreeOfParallelism = 4 },
                async (batch, token) =>
                {
                    await SendGate.WaitAsync(token);
                    try
                    {
                        var result = await SendBatchWithRetryAsync(batch, title, body, appToken, appId, token);
                        foreach (var device in batch)
                            outcomes.Add(new PushDeliveryOutcome(device, result.Status, result.Exception));
                    }
                    finally
                    {
                        SendGate.Release();
                    }
                });

            return outcomes.ToArray();
        }

        private async Task<BatchSendResult> SendBatchAsync(
            IReadOnlyCollection<PushDevice> devices,
            string title,
            string body,
            string appToken,
            string appId,
            CancellationToken cancellationToken)
        {
            var androidTokens = devices.Where(d => d.Platform == DevicePlatform.Android).Select(d => d.Token).ToList();
            var iosTokens = devices.Where(d => d.Platform == DevicePlatform.iOS).Select(d => d.Token).ToList();
            if (androidTokens.Count == 0 && iosTokens.Count == 0)
                return new BatchSendResult(PushDeliveryStatus.PermanentFailure,
                    new InvalidOperationException("Pushe batch contains no supported device platforms."));

            var payload = new Dictionary<string, object?>
            {
                ["app_ids"] = new[] { appId },
                ["title"] = title,
                ["content"] = body
            };

            if (androidTokens.Count > 0)
                payload["android_tokens"] = androidTokens;
            if (iosTokens.Count > 0)
                payload["ios_tokens"] = iosTokens;

            using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.pushe.co/v2/messaging/notifications/");
            request.Headers.TryAddWithoutValidation("Authorization", $"Token {appToken}");
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            try
            {
                using var response = await httpClient.SendAsync(request, cancellationToken);
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                if (response.IsSuccessStatusCode)
                    return new BatchSendResult(PushDeliveryStatus.Accepted);

                var exception = new HttpRequestException(
                    $"Pushe push failed: {response.StatusCode} - {responseBody}",
                    null,
                    response.StatusCode);
                return new BatchSendResult(
                    IsTransientStatusCode(response.StatusCode)
                        ? PushDeliveryStatus.TransientFailure
                        : PushDeliveryStatus.PermanentFailure,
                    exception);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (OperationCanceledException exception)
            {
                return new BatchSendResult(PushDeliveryStatus.TransientFailure, exception);
            }
            catch (HttpRequestException exception)
            {
                return new BatchSendResult(PushDeliveryStatus.TransientFailure, exception);
            }
            catch (Exception exception)
            {
                return new BatchSendResult(PushDeliveryStatus.PermanentFailure, exception);
            }
        }

        private async Task<BatchSendResult> SendBatchWithRetryAsync(
            IReadOnlyCollection<PushDevice> devices,
            string title,
            string body,
            string appToken,
            string appId,
            CancellationToken cancellationToken)
        {
            BatchSendResult result = new(PushDeliveryStatus.PermanentFailure);
            for (var attempt = 1; attempt <= 3; attempt++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                result = await SendBatchAsync(devices, title, body, appToken, appId, cancellationToken);
                if (result.Status != PushDeliveryStatus.TransientFailure || attempt == 3)
                    return result;

                var baseDelayMs = 250 * Math.Pow(2, attempt - 1);
                var jitteredDelayMs = baseDelayMs + Random.Shared.NextDouble() * baseDelayMs * 0.5;
                await Task.Delay(TimeSpan.FromMilliseconds(jitteredDelayMs), cancellationToken);
            }

            return result;
        }

        private static bool IsTransientStatusCode(HttpStatusCode statusCode)
            => statusCode == HttpStatusCode.RequestTimeout ||
               (int)statusCode == 429 ||
               (int)statusCode >= 500;

        private sealed record BatchSendResult(PushDeliveryStatus Status, Exception? Exception = null);
    }
}
