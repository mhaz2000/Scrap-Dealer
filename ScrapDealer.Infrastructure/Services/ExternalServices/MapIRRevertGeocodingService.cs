using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ScrapDealer.Application.Services.ExternalServices;

namespace ScrapDealer.Infrastructure.Services.ExternalServices
{
    internal class MapIRRevertGeocodingService(IConfiguration configuration, HttpClient httpClient) : IMapIRRevertGeocodingService
    {
        private readonly string endPoint = configuration["MapIR:Address"] ?? throw new Exception("Map.ir endpoint not found.");
        private readonly string apiKey = configuration["MapIR:ApiKey"] ?? throw new Exception("Map.ir Payamak key not found.");

        public async Task<(AddressMapIRResponse? adderss, bool status)> GetAddressAsync(double latitude, double longitude)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{endPoint}?lat={latitude}&lon={longitude}");
            request.Headers.Add("x-api-key", apiKey);
            var response = await httpClient.SendAsync(request);
            var responseAsString = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();

                return (JsonConvert.DeserializeObject<AddressMapIRResponse>(responseAsString), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(responseAsString);
                return (null, false);
            }
        }
    }
}
