using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using ScrapDealer.Application.Services;
using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.Services.PushNotifications;

internal sealed record PushNotificationJob(
    string Title,
    string Body,
    IReadOnlyCollection<NotificationTarget>? Targets,
    IReadOnlyCollection<Guid>? UserIds);

internal interface IPushNotificationQueue
{
    ValueTask EnqueueAsync(PushNotificationJob job, CancellationToken cancellationToken);
    IAsyncEnumerable<PushNotificationJob> ReadAllAsync(CancellationToken cancellationToken);
}

internal sealed class PushNotificationQueue : IPushNotificationQueue
{
    private readonly Channel<PushNotificationJob> _channel =
        Channel.CreateBounded<PushNotificationJob>(new BoundedChannelOptions(1024)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false,
            AllowSynchronousContinuations = false
        });

    public ValueTask EnqueueAsync(PushNotificationJob job, CancellationToken cancellationToken)
        => _channel.Writer.WriteAsync(job, cancellationToken);

    public IAsyncEnumerable<PushNotificationJob> ReadAllAsync(CancellationToken cancellationToken)
        => _channel.Reader.ReadAllAsync(cancellationToken);
}

internal sealed class PushNotificationDispatcher(
    IPushNotificationQueue queue,
    ILogger<PushNotificationDispatcher> logger) : IPushNotificationDispatcher
{
    public async Task DispatchAsync(
        string title,
        string body,
        IReadOnlyCollection<NotificationTarget>? targets,
        IReadOnlyCollection<Guid>? userIds,
        CancellationToken cancellationToken)
    {
        var job = new PushNotificationJob(
            title,
            body,
            targets?.ToArray(),
            userIds?.Distinct().ToArray());

        await queue.EnqueueAsync(job, cancellationToken);
        logger.LogInformation(
            "Queued push notification for {TargetCount} role targets and {UserCount} users.",
            job.Targets?.Count ?? 0,
            job.UserIds?.Count ?? 0);
    }
}
