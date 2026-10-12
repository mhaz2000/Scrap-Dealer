namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface IPushNotificationService
    {
        /// <summary>
        /// Sends to each device and reports provider acceptance and per-device failures.
        /// Accepted means provider acceptance, not confirmed device delivery.
        /// </summary>
        Task<IReadOnlyCollection<PushDeliveryOutcome>> SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken);
    }
}
