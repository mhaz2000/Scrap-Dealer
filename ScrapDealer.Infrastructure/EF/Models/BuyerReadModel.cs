using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class BuyerReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool Verified { get; set; }
        public bool IsActive { get; set; }
        public bool IsWholeSaleBuyer { get; set; }
        public bool IsFixedLocation { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string NationalCode { get; set; }
        public required string Province { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }
        public string? InactiveReason { get; set; }
        public string? CompanyName { get; set; }
        public string? NumberPlate { get; set; }
        public required string AddressDescription { get; set; }
        public Gender Gender { get; set; }
        public ActivityArea ActivityArea { get; set; }
        public Guid ProfileFormFileId { get; set; }
        public Guid NationalCardFileId { get; set; }
        public Guid? BusinessLicenseFileId { get; set; }
        public Guid? CarCardFileId { get; set; }

        public required UserReadModel User { get; set; }
        public Guid UserId { get; set; }
        public float Score { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
