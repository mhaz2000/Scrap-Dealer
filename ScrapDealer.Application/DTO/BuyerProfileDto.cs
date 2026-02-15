namespace ScrapDealer.Application.DTO
{

    public record BuyerProfileDto : ProfileDto
    {
        public Guid UserId { get; set; }
    }
}
