namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface IPushNotificationService
    {
        /// <summary>
        /// Sends a push notification to the given devices, returning the registration tokens
        /// that the active provider reported as invalid/unregistered so the caller can
        /// deactivate them.
        /// </summary>
        Task SendAsync(string title, string body, IReadOnlyCollection<PushDevice> devices, CancellationToken cancellationToken);
    }
}