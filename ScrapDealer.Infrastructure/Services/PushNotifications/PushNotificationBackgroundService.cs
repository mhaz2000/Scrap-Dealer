using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ScrapDealer.Infrastructure.Services.PushNotifications;

internal interface IPushNotificationJobProcessor
{
    Task ProcessAsync(PushNotificationJob job, CancellationToken cancellationToken);
}

internal sealed class PushNotificationBackgroundService(
    IPushNotificationQueue queue,
    IServiceScopeFactory scopeFactory,
    ILogger<PushNotificationBackgroundService> logger) : BackgroundService
{
    private const int WorkerCount = 4;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => Task.WhenAll(Enumerable.Range(0, WorkerCount)
            .Select(_ => ProcessQueueAsync(stoppingToken)));

    private async Task ProcessQueueAsync(CancellationToken stoppingToken)
    {
        await foreach (var job in queue.ReadAllAsync(stoppingToken))
        {
            try
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var processor = scope.ServiceProvider.GetRequiredService<IPushNotificationJobProcessor>();
                await processor.ProcessAsync(job, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Push notification job failed.");
            }
        }
    }
}
