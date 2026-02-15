using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Notifications;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Notification : AggregateRoot<Guid>
    {
        public Title Title { get; set; }
        public NotificationContent Content { get; set; }
        public List<Guid> SeenBy { get; set; } = [];

        public Notification(Title title, NotificationContent content)
        {
            Title = title;
            Content = content;
        }

        public void Update(Title title, NotificationContent content)
        {
            Title = title;
            Content = content;
        }
    }
}
