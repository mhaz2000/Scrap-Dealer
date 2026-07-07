using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record SellerProfileDto : ProfileDto
    {
        public PersonType PersonType { get; set; }
        public Guid UserId { get; set; }
        public string? Email { get; set; }
    }
}
