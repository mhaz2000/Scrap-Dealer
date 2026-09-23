using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;

namespace ScrapDealer.Domain.Factories
{
    public class UserDeviceTokenFactory : IUserDeviceTokenFactory
    {
        public UserDeviceToken Create(Guid userId, string token, PushProvider provider, DevicePlatform platform)
            => new(userId, token, provider, platform);
    }
}