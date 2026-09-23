using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IUserDeviceTokenFactory
    {
        UserDeviceToken Create(Guid userId, string token, PushProvider provider, DevicePlatform platform);
    }
}