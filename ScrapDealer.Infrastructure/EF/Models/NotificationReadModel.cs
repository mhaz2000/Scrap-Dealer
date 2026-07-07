using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class NotificationReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public List<Guid> SeenBy { get; set; }
        public string Content { get; set; }
        public List<NotificationTarget> Targets { get; set; }
    }
}
