using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.Services
{
    public interface IPushNotificationDispatcher
    {
        Task DispatchAsync(string title, string body, IReadOnlyCollection<NotificationTarget>? targets, IReadOnlyCollection<Guid>? userIds, CancellationToken cancellationToken);
    }
}
