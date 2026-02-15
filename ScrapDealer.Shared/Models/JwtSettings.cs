namespace ScrapDealer.Shared.Models
{
    public sealed class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }

}
