namespace ScrapDealer.Application.DTO
{
    public record PanelAuthenticationDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public string RefreshToken { get; internal set; }
    }
}
