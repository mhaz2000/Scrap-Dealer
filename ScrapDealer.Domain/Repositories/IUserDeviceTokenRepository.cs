using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories.Base;

namespace ScrapDealer.Domain.Repositories
{
    public interface IUserDeviceTokenRepository : IGenericRepository<UserDeviceToken>
    {
        Task<UserDeviceToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
        Task RegisterOrActivateAsync(UserDeviceToken device, CancellationToken cancellationToken);
        Task<List<UserDeviceToken>> GetActiveByUserIdsAsync(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken);
        Task DeactivateFcmTokensAsync(IReadOnlyCollection<string> tokens, CancellationToken cancellationToken);
    }
}
