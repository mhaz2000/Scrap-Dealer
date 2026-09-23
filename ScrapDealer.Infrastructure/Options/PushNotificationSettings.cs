namespace ScrapDealer.Infrastructure.Options
{
    public class PushNotificationSettings
    {
        public string ActiveProvider { get; set; } = "Fcm";

        public FcmPushSettings Fcm { get; set; } = new();

        public PushePushSettings Pushe { get; set; } = new();
    }

    public class FcmPushSettings
    {
        public string? ProjectId { get; set; }

        public string? ServiceAccountJsonPath { get; set; }
    }

    public class PushePushSettings
    {
        public string? AppToken { get; set; }

        public string? AppId { get; set; }
    }
}
