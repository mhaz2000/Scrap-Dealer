using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Sellers
{
    public record CreateSellerCommand(string FirstName, string LastName, string NationalCode, string City, string Province,
        string PostalCode, string AddressDescription, string Email, PersonType PersonType, Gender Gender, Guid UserId) : ICommand;
}
