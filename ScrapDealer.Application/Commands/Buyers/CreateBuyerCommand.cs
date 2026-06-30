using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Buyers
{
    public record CreateBuyerCommand(string FirstName, string LastName, string NationalCode, string City,
        string Province, Gender Gender, ActivityArea ActivityArea, string? CompanyName, string? NumberPlate,
        string AddressDescription, Guid? BusinessLicenseFileId, Guid? CarCardFileId, Guid NationalCardFileId,
        Guid ProfileFormFileId, bool IsWholeSaleBuyer, bool IsFixedLocation, Guid UserId,
        double Latitude, double Longitude) : ICommand;

}
