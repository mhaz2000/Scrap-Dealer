using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories
{
    public class BuyerFactory : IBuyerFactory
    {

        public Buyer Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? companyName, string? numberPlate, string addressDescription, Gender gender,
            ActivityArea activityArea, Guid businessLicenseFileId, Guid nationalCardFileId, Guid profileFormFileId, bool isWholeSaleBuyer, bool isFixedLocation, Guid userId)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, string.Empty, addressDescription, activityArea);
            var companyNameValue = companyName is null ? null : CompanyName.Create(companyName);
            var numberPlateValue = numberPlate is null ? null : NumberPlate.Create(numberPlate);

            return new Buyer(personNameValue, nationalCodeValue, addressValue, companyNameValue,
                numberPlateValue, gender, businessLicenseFileId, nationalCardFileId, profileFormFileId, isWholeSaleBuyer, isFixedLocation, userId);
        }

        public Buyer Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? companyName, string? numberPlate, string addressDescription, Gender gender,
            ActivityArea activityArea, Guid businessLicenseFileId, Guid nationalCardFileId, Guid profileFormFileId, Buyer buyer)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, string.Empty, addressDescription, activityArea);
            var companyNameValue = companyName is null ? null : CompanyName.Create(companyName);
            var numberPlateValue = numberPlate is null ? null : NumberPlate.Create(numberPlate);

            buyer.Update(personNameValue, nationalCodeValue, addressValue, companyNameValue,
                numberPlateValue, gender, businessLicenseFileId, nationalCardFileId, profileFormFileId);

            return buyer;
        }
    }
}
