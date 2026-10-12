using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.Options;

namespace ScrapDealer.Infrastructure.Services.PushNotifications
{
    internal sealed class PushNotificationDispatcher(
        IOptions<PushNotificationSettings> settings,
        ReadDbContext readDbContext,
        IUserDeviceTokenRepository deviceTokenRepository,
        IServiceScopeFactory scopeFactory) : IPushNotificationDispatcher
    {
        public async Task DispatchAsync(string title, string body, IReadOnlyCollection<NotificationTarget> targets, CancellationToken cancellationToken)
        {
            var roleNames = targets
                .Select(MapTargetToRole)
                .Where(r => r is not null)
                .Cast<string>()
                .Distinct()
                .ToList();

            if (roleNames.Count == 0)
                return;

            var userIds = await readDbContext.UserRoles
                .Where(ur => roleNames.Contains(ur.Role.Name))
                .Select(ur => ur.UserId)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (userIds.Count == 0)
                return;

            var devices = await deviceTokenRepository.GetActiveByUserIdsAsync(userIds, cancellationToken);
            if (devices.Count == 0)
                return;

            var activeProvider = ParseProvider(settings.Value.ActiveProvider);

            var pushDevices = devices
                .Where(d => d.Provider == activeProvider)
                .Select(d => new PushDevice(d.Token, d.Platform, d.Provider))
                .ToList();

            if (pushDevices.Count == 0)
                return;

            _ = Task.Run(async () =>
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var pushNotificationService = scope.ServiceProvider
                    .GetRequiredService<IPushNotificationService>();

                try
                {
                    await pushNotificationService.SendAsync(title, body, pushDevices, CancellationToken.None);
                }
                catch
                {
                    // Push delivery must never fail the notification creation request.
                }
            });
        }

        private static string? MapTargetToRole(NotificationTarget target)
            => target switch
            {
                NotificationTarget.Seller => "Seller",
                NotificationTarget.Buyer => "Buyer",
                NotificationTarget.Support => "Support",
                _ => null
            };

        private static PushProvider ParseProvider(string provider)
            => provider.Equals("Pushe", StringComparison.OrdinalIgnoreCase)
                ? PushProvider.Pushe
                : PushProvider.Fcm;
    }
}
