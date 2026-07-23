using ScrapDealer.Application.DTO.External;

namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface INeshanRevertGeocodingService
    {
        Task<(AddressResponse? adderss, bool status)> GetAddressAsync(double latitude, double longitude);
    }
}
