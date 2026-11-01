using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Seen { get; set; }
        public bool IsPushEnabled { get; set; }
        public IReadOnlyList<Guid> UserIds { get; set; } = [];
        public IReadOnlyList<UserDto> Users { get; set; } = [];
        public List<NotificationTarget>? targets { get; set; }
    }

}
