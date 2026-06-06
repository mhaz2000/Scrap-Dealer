namespace ScrapDealer.Application.DTO
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string? WalletNumber { get; set; }
        public decimal? WalletBalance { get; set; }
    }
}
