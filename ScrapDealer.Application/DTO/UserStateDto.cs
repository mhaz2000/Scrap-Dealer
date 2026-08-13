namespace ScrapDealer.Application.DTO
{
    public record UserStateDto
    {
        public Guid? BuyerId { get; set; }
        public Guid? SellerId { get; set; }
        public bool Verified { get; set; }
        public bool IsActive { get; set; }
    }
}
