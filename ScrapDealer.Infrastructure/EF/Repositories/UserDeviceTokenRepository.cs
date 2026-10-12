using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;
using System.Data;

namespace ScrapDealer.Infrastructure.EF.Repositories
{
    internal class UserDeviceTokenRepository : GenericRepository<UserDeviceToken>, IUserDeviceTokenRepository
    {
        public UserDeviceTokenRepository(WriteDbContext context) : base(context)
        {
        }

        public async Task<UserDeviceToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken)
            => await _context.UserDeviceTokens
                .FirstOrDefaultAsync(
                    d => d.Token == token,
                    cancellationToken);

        public async Task RegisterOrActivateAsync(UserDeviceToken device, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken);

            var existing = await _context.UserDeviceTokens
                .FirstOrDefaultAsync(
                    item => item.UserId == device.UserId && item.Token == device.Token,
                    cancellationToken);

            if (existing is null)
                await _context.UserDeviceTokens.AddAsync(device, cancellationToken);
            else
                existing.Activate();

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        public async Task<List<UserDeviceToken>> GetActiveByUserIdsAsync(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken)
            => await _context.UserDeviceTokens
                .Where(d => d.IsActive && userIds.Contains(d.UserId))
                .ToListAsync(cancellationToken);

        public async Task DeactivateFcmTokensAsync(IReadOnlyCollection<string> tokens, CancellationToken cancellationToken)
        {
            if (tokens.Count == 0)
                return;

            var tokenSet = tokens.ToHashSet(StringComparer.Ordinal);
            var devices = await _context.UserDeviceTokens
                .Where(device => device.IsActive && device.Provider == ScrapDealer.Domain.Consts.PushProvider.Fcm && tokenSet.Contains(device.Token))
                .ToListAsync(cancellationToken);

            foreach (var device in devices)
                device.Deactivate();

            if (devices.Count > 0)
                await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
