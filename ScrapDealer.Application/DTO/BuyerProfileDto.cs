namespace ScrapDealer.Application.DTO
{

    public record BuyerProfileDto : ProfileDto
    {
        public Guid UserId { get; set; }
        public Guid? BusinessLicenseFileId { get; set; }
        public Guid? CarCardFileId { get; set; }
        public IEnumerable<Guid> LocationImages { get; set; }

    }
}
