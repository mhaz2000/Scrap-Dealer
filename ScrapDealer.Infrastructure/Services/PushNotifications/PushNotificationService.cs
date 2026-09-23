using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.Services.PushNotifications
{
    public class PushNotificationService(IEnumerable<IPushNotificationProvider> providers) : IPushNotificationService
    {
        private const int MaxFcmAttempts = 3; // initial send + 2 retries

        private readonly List<IPushNotificationProvider> _providers = providers.ToList();

        public async Task SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken)
        {
            if (devices.Count == 0)
                return;

            // Every non-FCM provider handles only its own devices (Pushe direct, etc.).
            foreach (var provider in _providers.Where(p => p.Provider != PushProvider.Fcm))
            {
                var providerDevices = devices.Where(d => d.Provider == provider.Provider).ToList();
                if (providerDevices.Count == 0)
                    continue;

                await provider.SendAsync(title, body, providerDevices, cancellationToken);
            }

            // FCM is the primary channel for FCM-registered devices: send, retrying up to
            // 2 more times on failure, then fall back to Pushe if it still fails.
            var fcmDevices = devices.Where(d => d.Provider == PushProvider.Fcm).ToList();
            if (fcmDevices.Count == 0)
                return;

            var fcm = _providers.FirstOrDefault(p => p.Provider == PushProvider.Fcm);
            if (fcm is null)
                return;

            Exception? lastError = null;
            for (var attempt = 1; attempt <= MaxFcmAttempts; attempt++)
            {
                try
                {
                    await fcm.SendAsync(title, body, fcmDevices, cancellationToken);
                    return;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                    if (attempt < MaxFcmAttempts)
                        await Task.Delay(TimeSpan.FromSeconds(attempt), cancellationToken);
                }
            }

            // FCM exhausted its retries -> fall back to Pushe for these devices.
            var pushe = _providers.FirstOrDefault(p => p.Provider == PushProvider.Pushe);
            if (pushe is not null)
            {
                await pushe.SendAsync(title, body, fcmDevices, cancellationToken);
                return;
            }

            throw lastError ?? new InvalidOperationException("FCM push failed after all attempts.");
        }
    }
}
