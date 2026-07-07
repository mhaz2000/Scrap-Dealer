using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record ProfileDto
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string NationalCode { get; set; }
        public required string Province { get; set; }
        public required string City { get; set; }
        public required string AddressDescription { get; set; }
        public required string Phone { get; set; }
        public string? InactiveReason { get; set; }
        public Gender Gender { get; set; }
        public ActivityArea ActivityArea { get; set; }
        public string? CompanyName { get; set; }
        public string? NumberPlate { get; set; }
        public Guid? NationalCardFileId { get; set; }
        public Guid? ProfileFormFileId { get; set; }
        public bool Verified { get; set; }
        public bool IsActive { get; set; }
        public string? WalletNumber { get; set; }
        public decimal? WalletBalance { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
