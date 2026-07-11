using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class ReferralReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ReferrerUserId { get; set; }
        public UserReadModel ReferrerUser { get; set; }
        public Guid RefereeUserId { get; set; }
        public UserReadModel RefereeUser { get; set; }
        public string Code { get; set; }
        public ReferralStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
