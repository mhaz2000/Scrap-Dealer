using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories
{
    public class SellerFactory : ISellerFactory
    {
        public Seller Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string postalCode, string addressDescription, Email email, Gender gender, PersonType personType, Guid userId)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, postalCode, addressDescription, null);
            var emailValue = Email.Create(email);

            return new(personNameValue, nationalCodeValue, addressValue, emailValue, personType, gender, userId);
        }

        public Seller Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string postalCode, string addressDescription, Email email, Gender gender, PersonType personType, Seller buyer)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = ProfileAddress.Create(province, city, postalCode, addressDescription, null);
            var emailValue = Email.Create(email);

            buyer.Update(personNameValue, nationalCodeValue, addressValue, email, personType, gender);

            return buyer;
        }
    }
}
