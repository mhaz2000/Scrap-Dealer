using Microsoft.Extensions.Options;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.Options;
using System.Text;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.Services.PushNotifications
{
    public class PushePushNotificationProvider(HttpClient httpClient, IOptions<PushNotificationSettings> settings)
        : IPushNotificationProvider
    {
        public PushProvider Provider => PushProvider.Pushe;

        public async Task<IReadOnlyCollection<string>> SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken)
        {
            var appToken = settings.Value.Pushe.AppToken ?? throw new InvalidOperationException("Pushe AppToken is not configured.");
            var appId = settings.Value.Pushe.AppId ?? throw new InvalidOperationException("Pushe AppId is not configured.");

            var androidTokens = devices.Where(d => d.Platform == DevicePlatform.Android).Select(d => d.Token).ToList();
            var iosTokens = devices.Where(d => d.Platform == DevicePlatform.iOS).Select(d => d.Token).ToList();

            if (androidTokens.Count == 0 && iosTokens.Count == 0)
                return [];

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

            using var response = await httpClient.SendAsync(request, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Pushe push failed: {response.StatusCode} - {responseBody}");

            return [];
        }
    }
}