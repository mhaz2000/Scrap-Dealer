using ScrapDealer.Application.DTO.External;

namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface IMapIRRevertGeocodingService
    {
        Task<(AddressMapIRResponse? adderss, bool status)> GetAddressAsync(double latitude, double longitude);
    }
}
