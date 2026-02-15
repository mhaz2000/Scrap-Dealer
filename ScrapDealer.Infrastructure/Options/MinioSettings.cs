namespace ScrapDealer.Infrastructure.Options
{
    internal class MinioSettings
    {
        public string Endpoint { get; set; } = "localhost:9000";
        public string AccessKey { get; set; } = "admin";
        public string SecretKey { get; set; } = "admin123";
        public string BucketName { get; set; } = "filestorage";
        public bool UseSSL { get; set; } = false;
    }
}
