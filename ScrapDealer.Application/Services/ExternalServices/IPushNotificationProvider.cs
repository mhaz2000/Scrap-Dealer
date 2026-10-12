using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.Services.ExternalServices
{
    public record PushDevice(string Token, DevicePlatform Platform, PushProvider Provider);

    public enum PushDeliveryStatus
    {
        Accepted,
        InvalidToken,
        TransientFailure,
        PermanentFailure
    }

    public sealed record PushDeliveryOutcome(
        PushDevice Device,
        PushDeliveryStatus Status,
        Exception? Exception = null);

    public interface IPushNotificationProvider
    {
        PushProvider Provider { get; }

        /// <summary>
        /// Sends to each device and reports provider acceptance, invalid tokens, or failures.
        /// Accepted means the provider accepted the request; it does not confirm device delivery.
        /// </summary>
        Task<IReadOnlyCollection<PushDeliveryOutcome>> SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken);
    }
}
