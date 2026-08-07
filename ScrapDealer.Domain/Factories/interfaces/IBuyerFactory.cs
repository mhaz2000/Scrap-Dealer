using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IBuyerFactory
    {
        Buyer Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? companyName, string? numberPlate, string addressDescription, Gender gender, ActivityArea ActivityArea,
            Guid? businessLicenseFileId, Guid NationalCardFileId, Guid ProfileFormFileId, Guid? CarCardFileId, bool isWholeSaleBuyer,
            bool isFixedLocation, IEnumerable<Guid> LocationImages, User user, double? latitude, double? longitude, Code code);

        Buyer Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? companyName, string? numberPlate, string addressDescription, Gender gender, ActivityArea ActivityArea,
            Guid? businessLicenseFileId, Guid NationalCardFileId, Guid ProfileFormFileId, Guid? CarCardFileId, Buyer buyer,
            double? latitude, double? longitude);
    }
}
