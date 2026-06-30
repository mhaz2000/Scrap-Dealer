using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record NearbyBuyerDto
    {
        public Guid Id { get; set; }
        public required string Province { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public string? CompanyName { get; set; }
        public string? NumberPlate { get; set; }
        public required string AddressDescription { get; set; }
        public Gender Gender { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsWholeSaleBuyer { get; set; }
        public float Score { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
