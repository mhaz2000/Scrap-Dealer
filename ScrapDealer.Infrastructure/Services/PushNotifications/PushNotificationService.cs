using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.Options;

namespace ScrapDealer.Infrastructure.Services.PushNotifications
{
    public class PushNotificationService(
        IEnumerable<IPushNotificationProvider> providers,
        IOptions<PushNotificationSettings> settings,
        ILogger<PushNotificationService> logger) : IPushNotificationService
    {
        private readonly List<IPushNotificationProvider> _providers = providers.ToList();

        public async Task<IReadOnlyCollection<PushDeliveryOutcome>> SendAsync(
            string title,
            string body,
            IReadOnlyCollection<PushDevice> devices,
            CancellationToken cancellationToken)
        {
            if (devices.Count == 0)
                return [];

            var activeProvider = ParseActiveProvider(settings.Value.ActiveProvider);
            if (activeProvider == PushProvider.Pushe)
            {
                var pushe = GetProvider(PushProvider.Pushe);
                return await pushe.SendAsync(title, body, devices, cancellationToken);
            }

            var results = new List<PushDeliveryOutcome>();
            var pusheDevices = devices.Where(device => device.Provider == PushProvider.Pushe).ToList();
            var fcmDevices = devices.Where(device => device.Provider == PushProvider.Fcm).ToList();
            if (fcmDevices.Count == 0)
            {
                if (pusheDevices.Count == 0)
                    return results;

                return await GetProvider(PushProvider.Pushe)
                    .SendAsync(title, body, pusheDevices, cancellationToken);
            }

            var fcm = GetProvider(PushProvider.Fcm);
            var fcmResults = await fcm.SendAsync(title, body, fcmDevices, cancellationToken);
            var fallbackDevices = fcmResults
                .Where(result => result.Status == PushDeliveryStatus.TransientFailure)
                .Select(result => result.Device)
                .ToList();

            results.AddRange(fcmResults.Where(result => result.Status != PushDeliveryStatus.TransientFailure));

            if (fallbackDevices.Count == 0)
            {
                results.AddRange(fcmResults.Where(result => result.Status == PushDeliveryStatus.TransientFailure));
            }
            else
            {
                var pusheSettings = settings.Value.Pushe;
                if (string.IsNullOrWhiteSpace(pusheSettings.AppId) ||
                    string.IsNullOrWhiteSpace(pusheSettings.AppToken))
                {
                    logger.LogWarning(
                        "FCM had {Count} retryable failures, but Pushe fallback is not configured.",
                        fallbackDevices.Count);
                    results.AddRange(fcmResults.Where(result => result.Status == PushDeliveryStatus.TransientFailure));
                }
                else
                {
                    logger.LogInformation(
                        "Routing {Count} FCM devices to Pushe after retryable FCM failures.",
                        fallbackDevices.Count);
                    pusheDevices.AddRange(fallbackDevices);
                }
            }

            if (pusheDevices.Count > 0)
            {
                var pusheResults = await GetProvider(PushProvider.Pushe)
                    .SendAsync(title, body, pusheDevices, cancellationToken);
                results.AddRange(pusheResults);
            }

            return results;
        }

        private IPushNotificationProvider GetProvider(PushProvider provider)
            => _providers.FirstOrDefault(candidate => candidate.Provider == provider)
               ?? throw new InvalidOperationException($"Push provider {provider} is not registered.");

        private static PushProvider ParseActiveProvider(string activeProvider)
        {
            if (string.Equals(activeProvider, "Fcm", StringComparison.OrdinalIgnoreCase))
                return PushProvider.Fcm;
            if (string.Equals(activeProvider, "Pushe", StringComparison.OrdinalIgnoreCase))
                return PushProvider.Pushe;

            throw new InvalidOperationException($"Unsupported active push provider '{activeProvider}'.");
        }
    }
}
