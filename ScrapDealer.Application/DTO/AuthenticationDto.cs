namespace ScrapDealer.Application.DTO
{
    public record AuthenticationDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
