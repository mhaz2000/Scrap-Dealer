using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Notifications;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface INotificationFactory
    {
        Notification Create(Title title, NotificationContent notificationContent);
        Notification Update(Title title, NotificationContent notificationContent, Notification notification);
    }
}
