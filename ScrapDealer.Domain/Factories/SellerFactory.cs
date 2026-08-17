using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories
{
    public class SellerFactory : ISellerFactory
    {
        public Seller Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? postalCode, string addressDescription, Email? email, Gender gender, PersonType personType, User user, Guid? nationalCardFileId, Guid? profileFormFileId, Code code,
            double? latitude = null, double? longitude = null)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, postalCode, addressDescription, null);
            var emailValue = Email.Create(email);
            var locationValue = latitude.HasValue && longitude.HasValue ? Location.Create(latitude.Value, longitude.Value) : null;

            return new(personNameValue, nationalCodeValue, addressValue, emailValue, personType, gender, user, nationalCardFileId, profileFormFileId, code, locationValue);
        }

        public Seller Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string? postalCode, string addressDescription, Email? email, Gender gender, PersonType personType, Guid? nationalCardFileId, Guid? profileFormFileId, Seller buyer,
            double? latitude = null, double? longitude = null)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, postalCode, addressDescription, null);
            var emailValue = Email.Create(email);
            var locationValue = latitude.HasValue && longitude.HasValue ? Location.Create(latitude.Value, longitude.Value) : null;

            buyer.Update(personNameValue, nationalCodeValue, addressValue, email, personType, gender, nationalCardFileId, profileFormFileId, locationValue);

            return buyer;
        }
    }
}