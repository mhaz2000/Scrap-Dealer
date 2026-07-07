using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Notifications;

namespace ScrapDealer.Domain.Factories
{
    public class NotificationFactory : INotificationFactory
    {
        public Notification Create(Title title, NotificationContent notificationContent, List<NotificationTarget> targets)
        {
            var titleValue = Title.Create(title);
            var notificationContentValue = NotificationContent.Create(notificationContent);

            return new Notification(titleValue, notificationContentValue, targets);
        }

        public Notification Update(Title title, NotificationContent notificationContent, List<NotificationTarget> targets, Notification notification)
        {
            var titleValue = Title.Create(title);
            var notificationContentValue = NotificationContent.Create(notificationContent);

            notification.Update(titleValue, notificationContentValue, targets);
            return notification;
        }
    }
}
