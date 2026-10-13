using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Notifications;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Notification : AggregateRoot<Guid>
    {
        public Title Title { get; set; }
        public NotificationContent Content { get; set; }
        public List<NotificationTarget>? Targets { get; set; }
        public List<Guid>? UserIds { get; set; }
        public List<Guid> SeenBy { get; set; } = [];
        public bool IsPushEnabled { get; set; }
        public DateTime CreatedAt { get; private set; }

        public Notification(Title title, NotificationContent content, List<NotificationTarget>? targets, bool isPushEnabled, List<Guid>? userIds = null)
        {
            Title = title;
            Content = content;
            Targets = targets;
            UserIds = userIds;
            IsPushEnabled = isPushEnabled;
            CreatedAt = DateTime.Now;
        }

        public void Update(Title title, NotificationContent content, List<NotificationTarget>? targets, bool isPushEnabled, List<Guid>? userIds = null)
        {
            Title = title;
            Content = content;
            Targets = targets;
            UserIds = userIds;
            IsPushEnabled = isPushEnabled;
        }
    }
}
