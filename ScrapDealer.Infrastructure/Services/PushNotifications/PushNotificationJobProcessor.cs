using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;

namespace ScrapDealer.Infrastructure.Services.PushNotifications;

internal sealed class PushNotificationJobProcessor(
    ReadDbContext readDbContext,
    IUserDeviceTokenRepository deviceTokenRepository,
    IPushNotificationService pushNotificationService,
    ILogger<PushNotificationJobProcessor> logger) : IPushNotificationJobProcessor
{
    public async Task ProcessAsync(PushNotificationJob job, CancellationToken cancellationToken)
    {
        var userIds = job.UserIds?.Distinct().ToList() ?? [];
        var roleNames = (job.Targets ?? [])
            .Select(MapTargetToRole)
            .Where(role => role is not null)
            .Cast<string>()
            .Distinct()
            .ToList();

        if (roleNames.Count > 0)
        {
            var roleUserIds = await readDbContext.UserRoles
                .Where(userRole => roleNames.Contains(userRole.Role.Name))
                .Select(userRole => userRole.UserId)
                .Distinct()
                .ToListAsync(cancellationToken);

            userIds.AddRange(roleUserIds);
            userIds = userIds.Distinct().ToList();
        }

        if (userIds.Count == 0)
            return;

        var devices = await deviceTokenRepository.GetActiveByUserIdsAsync(userIds, cancellationToken);
        var pushDevices = devices
            .Select(device => new PushDevice(device.Token, device.Platform, device.Provider))
            .ToList();

        if (pushDevices.Count > 0)
        {
            var outcomes = await pushNotificationService.SendAsync(job.Title, job.Body, pushDevices, cancellationToken);
            var invalidTokens = outcomes
                .Where(outcome => outcome.Status == PushDeliveryStatus.InvalidToken)
                .Select(outcome => outcome.Device.Token)
                .Distinct(StringComparer.Ordinal)
                .ToList();

            if (invalidTokens.Count > 0)
            {
                await deviceTokenRepository.DeactivateFcmTokensAsync(invalidTokens, cancellationToken);
                logger.LogInformation("Deactivated {Count} invalid FCM device tokens.", invalidTokens.Count);
            }

            var failures = outcomes
                .Where(outcome => outcome.Status is PushDeliveryStatus.TransientFailure or PushDeliveryStatus.PermanentFailure)
                .ToList();
            foreach (var group in failures.GroupBy(failure => (failure.Device.Provider, failure.Status)))
            {
                logger.LogWarning(
                    group.First().Exception,
                    "Push provider {Provider} reported {Status} for {Count} devices.",
                    group.Key.Provider,
                    group.Key.Status,
                    group.Count());
            }
        }
    }

    private static string? MapTargetToRole(NotificationTarget target)
        => target switch
        {
            NotificationTarget.Seller => "Seller",
            NotificationTarget.Buyer => "Buyer",
            NotificationTarget.Support => "Support",
            _ => null
        };

}
