using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Notifications;

namespace ScrapDealer.Domain.Factories
{
    public class NotificationFactory : INotificationFactory
    {
        public Notification Create(Title title, NotificationContent notificationContent)
        {
            var titleValue = Title.Create(title);
            var notificationContentValue = NotificationContent.Create(notificationContent);

            return new Notification(titleValue, notificationContentValue);
        }

        public Notification Update(Title title, NotificationContent notificationContent, Notification notification)
        {
            var titleValue = Title.Create(title);
            var notificationContentValue = NotificationContent.Create(notificationContent);

            notification.Update(titleValue, notificationContentValue);
            return notification;
        }
    }
}
