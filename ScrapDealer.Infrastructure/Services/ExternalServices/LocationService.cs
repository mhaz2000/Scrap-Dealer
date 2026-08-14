using ScrapDealer.Application.Commands.Addresses;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Infrastructure.Services.ExternalServices
{
    public class LocationService(
            IMapIRRevertGeocodingService mapIRRevertGeocodingService,
            IRedisCacheService cacheService) : ILocationService
    {
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromDays(7);
        private const int RoundPrecision = 3;

        private static string RoundCoordinate(double value)
            => Math.Round(value, RoundPrecision).ToString("F" + RoundPrecision);
        public async Task<AddressMapIRResponse> GetLocationAsync(LocationCommand request)
        {
            var cacheKey = $"geo:{RoundCoordinate(request.Latitude)}_{RoundCoordinate(request.Longitude)}";

            var cached = cacheService.Get<AddressMapIRResponse>(cacheKey);
            if (cached is not null)
                return cached;

            var response = await mapIRRevertGeocodingService.GetAddressAsync(request.Latitude, request.Longitude);
            if (response.status)
            {
                cacheService.Set(cacheKey, response.adderss!, CacheExpiration);
                return response.adderss!;
            }
            else
                throw new BusinessException("سرویس بازیابی آدرس در دسترس نمی‌باشد.");
        }

    }
}
