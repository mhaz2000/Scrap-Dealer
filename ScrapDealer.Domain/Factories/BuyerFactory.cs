using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories
{
    public class BuyerFactory : IBuyerFactory
    {

        public Buyer Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? companyName, string? numberPlate, string addressDescription, Gender gender,
            ActivityArea activityArea, Guid? businessLicenseFileId, Guid nationalCardFileId, Guid profileFormFileId, Guid? carCardFileId, bool isWholeSaleBuyer, bool isFixedLocation, User user,
            double? latitude, double? longitude, Code code)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, string.Empty, addressDescription, activityArea);
            var companyNameValue = companyName is null ? null : CompanyName.Create(companyName);
            var numberPlateValue = string.IsNullOrEmpty(numberPlate) ? null : NumberPlate.Create(numberPlate);
            var locationValue = latitude.HasValue && longitude.HasValue ? Location.Create(latitude.Value, longitude.Value) : null;

            return new Buyer(personNameValue, nationalCodeValue, addressValue, companyNameValue,
                numberPlateValue, gender, businessLicenseFileId, nationalCardFileId, profileFormFileId, carCardFileId, isWholeSaleBuyer, isFixedLocation, user, locationValue, code);
        }

        public Buyer Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? companyName, string? numberPlate, string addressDescription, Gender gender,
            ActivityArea activityArea, Guid? businessLicenseFileId, Guid nationalCardFileId, Guid profileFormFileId, Guid? carCardFileId, Buyer buyer,
            double? latitude, double? longitude)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, string.Empty, addressDescription, activityArea);
            var companyNameValue = companyName is null ? null : CompanyName.Create(companyName);
            var numberPlateValue = numberPlate is null ? null : NumberPlate.Create(numberPlate);
            var locationValue = latitude.HasValue && longitude.HasValue ? Location.Create(latitude.Value, longitude.Value) : null;

            buyer.Update(personNameValue, nationalCodeValue, addressValue, companyNameValue,
                numberPlateValue, gender, businessLicenseFileId, nationalCardFileId, profileFormFileId, carCardFileId, locationValue);

            return buyer;
        }
    }
}
