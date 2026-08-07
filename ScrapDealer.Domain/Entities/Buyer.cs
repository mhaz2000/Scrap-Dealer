using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.SaleOrders;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Buyer : AggregateRoot<Guid>
    {
        public PersonName PersonName { get; private set; }
        public NationalCode NationalCode { get; private set; }
        public ProfileAddress Address { get; private set; }
        public CompanyName? CompanyName { get; private set; }
        public NumberPlate? NumberPlate { get; private set; }
        public Gender Gender { get; private set; }
        public string? InactiveReason { get; set; }
        public Code Code { get; private set; }

        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ProfileFormFileId { get; private set; }
        public Guid NationalCardFileId { get; private set; }
        public Guid? BusinessLicenseFileId { get; private set; }
        public Guid? CarCardFileId { get; private set; }
        public IEnumerable<Guid> LocationImages { get; private set; }
        public bool Verified { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsWholeSaleBuyer { get; private set; }
        public bool IsFixedLocation { get; private set; }
        public Score Score { get; set; }
        public Location? Location { get; private set; }

        public Buyer()
        {

        }

        public Buyer(PersonName personName, NationalCode nationalCode, ProfileAddress address, CompanyName? companyName, NumberPlate? numberPlate,
            Gender gender, Guid? businessLicenseFileId, Guid nationalCardFileId, Guid profileFormFileId, Guid? carCardFileId, bool isWholeSaleBuyer,
            bool isFixedLocation, IEnumerable<Guid> locationImages, User user, Location? location, Code code)
        {
            Id = Guid.NewGuid();
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Gender = gender;
            UserId = user.Id;
            CompanyName = companyName;
            NumberPlate = numberPlate;
            BusinessLicenseFileId = businessLicenseFileId;
            NationalCardFileId = nationalCardFileId;
            ProfileFormFileId = profileFormFileId;
            Verified = false;
            IsFixedLocation = isFixedLocation;
            CarCardFileId = carCardFileId;
            IsWholeSaleBuyer = isWholeSaleBuyer;
            IsActive = true;
            Score = 0;
            Location = location;
            Code = code;
            user.PersonName = personName;
            LocationImages = locationImages;
        }

        public void Update(PersonName personName, NationalCode nationalCode,
            ProfileAddress address, CompanyName? companyName, NumberPlate? numberPlate, Gender gender,
            Guid? businessLicenseFileId, Guid nationalCardFileId, Guid profileFormFileId, Guid? carCardFileId, Location? location)
        {
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Gender = gender;
            NumberPlate = numberPlate;
            CompanyName = companyName;
            BusinessLicenseFileId = businessLicenseFileId;
            NationalCardFileId = nationalCardFileId;
            ProfileFormFileId = profileFormFileId;
            CarCardFileId = carCardFileId;
            Location = location;
            User.PersonName = personName;
        }

        public void SetAsVerified() => Verified = true;
        public void SetActivationStatus(bool status, string? reason)
        {
            IsActive = status;
            InactiveReason = reason;
        }
    }
}
