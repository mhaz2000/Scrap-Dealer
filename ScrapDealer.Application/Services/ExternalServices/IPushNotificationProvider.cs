using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.Services.ExternalServices
{
    public record PushDevice(string Token, DevicePlatform Platform, PushProvider Provider);

    public interface IPushNotificationProvider
    {
        PushProvider Provider { get; }

        /// <summary>
        /// Sends a push notification to the given devices. Returns the registration tokens the
        /// provider reported as permanently invalid/unregistered (and therefore should be
        /// deactivated by the caller). Delivery failures are surfaced as exceptions so the
        /// composite can retry or fall back to another provider.
        /// </summary>
        Task<IReadOnlyCollection<string>> SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken);
    }
}