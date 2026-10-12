using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.Options;
using ScrapDealer.Infrastructure.Services.PushNotifications;
using Shouldly;

namespace ScrapDealer.UnitTests.Applications;

public class PushNotificationServiceTests
{
    [Fact]
    public async Task FcmActive_RoutesOnlyRetryableFcmFailuresToPushe()
    {
        var fcm = new StubProvider(PushProvider.Fcm, device => device.Token switch
        {
            "accepted" => Outcome(device, PushDeliveryStatus.Accepted),
            "invalid" => Outcome(device, PushDeliveryStatus.InvalidToken),
            "permanent" => Outcome(device, PushDeliveryStatus.PermanentFailure),
            _ => Outcome(device, PushDeliveryStatus.TransientFailure)
        });
        var pushe = new StubProvider(PushProvider.Pushe, device => Outcome(device, PushDeliveryStatus.Accepted));
        var sut = CreateService("Fcm", fcm, pushe);
        var devices = new[]
        {
            Device("accepted", PushProvider.Fcm),
            Device("retryable", PushProvider.Fcm),
            Device("invalid", PushProvider.Fcm),
            Device("permanent", PushProvider.Fcm),
            Device("pushe-direct", PushProvider.Pushe)
        };

        var outcomes = await sut.SendAsync("title", "body", devices, CancellationToken.None);

        fcm.Batches.ShouldHaveSingleItem();
        fcm.Batches[0].ShouldContain("accepted");
        fcm.Batches[0].ShouldContain("retryable");
        fcm.Batches[0].ShouldContain("invalid");
        fcm.Batches[0].ShouldContain("permanent");
        fcm.Batches[0].ShouldNotContain("pushe-direct");
        pushe.Batches.ShouldHaveSingleItem();
        pushe.Batches[0].Count.ShouldBe(2);
        pushe.Batches[0].ShouldContain("retryable");
        pushe.Batches[0].ShouldContain("pushe-direct");
        outcomes.Single(outcome => outcome.Device.Token == "invalid").Status.ShouldBe(PushDeliveryStatus.InvalidToken);
        outcomes.Single(outcome => outcome.Device.Token == "permanent").Status.ShouldBe(PushDeliveryStatus.PermanentFailure);
    }

    [Fact]
    public async Task PusheActive_DoesNotRouteAnyDevicesThroughFcm()
    {
        var fcm = new StubProvider(PushProvider.Fcm, device => Outcome(device, PushDeliveryStatus.Accepted));
        var pushe = new StubProvider(PushProvider.Pushe, device => Outcome(device, PushDeliveryStatus.Accepted));
        var sut = CreateService("Pushe", fcm, pushe);
        var devices = new[]
        {
            Device("registered-fcm", PushProvider.Fcm),
            Device("registered-pushe", PushProvider.Pushe)
        };

        var outcomes = await sut.SendAsync("title", "body", devices, CancellationToken.None);

        fcm.Batches.ShouldBeEmpty();
        pushe.Batches.ShouldHaveSingleItem();
        pushe.Batches[0].Count.ShouldBe(2);
        outcomes.Count.ShouldBe(2);
        outcomes.ShouldAllBe(outcome => outcome.Status == PushDeliveryStatus.Accepted);
    }

    private static PushNotificationService CreateService(
        string activeProvider,
        params IPushNotificationProvider[] providers)
        => new(
            providers,
            Options.Create(new PushNotificationSettings
            {
                ActiveProvider = activeProvider,
                Pushe = new PushePushSettings { AppId = "test-app", AppToken = "test-token" }
            }),
            NullLogger<PushNotificationService>.Instance);

    private static PushDevice Device(string token, PushProvider provider)
        => new(token, DevicePlatform.Android, provider);

    private static PushDeliveryOutcome Outcome(PushDevice device, PushDeliveryStatus status)
        => new(device, status);

    private sealed class StubProvider(
        PushProvider provider,
        Func<PushDevice, PushDeliveryOutcome> createOutcome) : IPushNotificationProvider
    {
        public PushProvider Provider => provider;
        public List<List<string>> Batches { get; } = [];

        public Task<IReadOnlyCollection<PushDeliveryOutcome>> SendAsync(
            string title,
            string body,
            IReadOnlyCollection<PushDevice> devices,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Batches.Add(devices.Select(device => device.Token).ToList());
            IReadOnlyCollection<PushDeliveryOutcome> outcomes = devices.Select(createOutcome).ToArray();
            return Task.FromResult(outcomes);
        }
    }
}
