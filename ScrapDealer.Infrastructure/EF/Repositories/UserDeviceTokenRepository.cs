using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories
{
    internal class UserDeviceTokenRepository : GenericRepository<UserDeviceToken>, IUserDeviceTokenRepository
    {
        public UserDeviceTokenRepository(WriteDbContext context) : base(context)
        {
        }

        public async Task<UserDeviceToken?> GetByTokenAsync(Guid userId, string token, CancellationToken cancellationToken)
            => await _context.UserDeviceTokens
                .FirstOrDefaultAsync(d => d.UserId == userId && d.Token == token, cancellationToken);

        public async Task<List<UserDeviceToken>> GetActiveByUserIdsAsync(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken)
            => await _context.UserDeviceTokens
                .Where(d => d.IsActive && userIds.Contains(d.UserId))
                .ToListAsync(cancellationToken);
    }
}