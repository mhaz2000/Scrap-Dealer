using ScrapDealer.Application.Commands.Addresses;

namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface ILocationService
    {
        Task<AddressMapIRResponse> GetLocationAsync(LocationCommand command);
    }
}
