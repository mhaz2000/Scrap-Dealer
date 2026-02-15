using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISellerFactory
    {
        Seller Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province, string postalCode,
            string addressDescription, Email email, Gender gender, PersonType personType, Guid userId);

        Seller Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province, string postalCode,
            string addressDescription, Email email, Gender gender, PersonType personType, Seller buyer);
    }
}
