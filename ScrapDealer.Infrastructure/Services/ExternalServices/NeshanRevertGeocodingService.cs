using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ScrapDealer.Application.DTO.External;
using ScrapDealer.Application.Services.ExternalServices;

namespace ScrapDealer.Infrastructure.Services.ExternalServices
{
    internal class NeshanRevertGeocodingService(IConfiguration configuration, HttpClient httpClient) : INeshanRevertGeocodingService
    {
        private readonly string endPoint = configuration["Neshan:Address"] ?? throw new Exception("Melli Payamak endpoint not found.");
        private readonly string apiKey = configuration["Neshan:ApiKey"] ?? throw new Exception("Melli Payamak key not found.");

        public async Task<(AddressResponse? adderss, bool status)> GetAddressAsync(double latitude, double longitude)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{endPoint}?lat={latitude}&lng={longitude}");
            request.Headers.Add("Api-Key", apiKey);
            var response = await httpClient.SendAsync(request);
            var responseAsString  = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();

                return (JsonConvert.DeserializeObject<AddressResponse>(responseAsString), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(responseAsString);
                return (null, false);
            }
        }
    }
}
