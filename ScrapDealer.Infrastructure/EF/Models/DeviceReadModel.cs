using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class DeviceReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public PushProvider Provider { get; set; }
        public DevicePlatform Platform { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}