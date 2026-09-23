using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories.Base;

namespace ScrapDealer.Domain.Repositories
{
    public interface IUserDeviceTokenRepository : IGenericRepository<UserDeviceToken>
    {
        Task<UserDeviceToken?> GetByTokenAsync(Guid userId, string token, CancellationToken cancellationToken);
        Task<List<UserDeviceToken>> GetActiveByUserIdsAsync(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken);
    }
}