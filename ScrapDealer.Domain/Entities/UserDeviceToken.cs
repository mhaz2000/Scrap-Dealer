using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class UserDeviceToken : AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public PushProvider Provider { get; private set; }
        public DevicePlatform Platform { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private UserDeviceToken() { }

        public UserDeviceToken(Guid userId, string token, PushProvider provider, DevicePlatform platform)
        {
            UserId = userId;
            Token = token;
            Provider = provider;
            Platform = platform;
            IsActive = true;
            CreatedAt = DateTime.Now;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.Now;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.Now;
        }
    }
}