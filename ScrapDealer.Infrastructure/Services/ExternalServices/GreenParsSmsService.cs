using Microsoft.Extensions.Configuration;
using ScrapDealer.Application.Services.ExternalServices;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.Services.ExternalServices
{

    public class GreenParsSmsService(HttpClient httpClient, IConfiguration configuration) : IGreenParsSmsService
    {
        private readonly string endPoint = configuration["GreenPars:Address"] ?? throw new Exception("Green Pars endpoint not found.");
        private readonly string apiKey = configuration["GreenPars:ApiKey"] ?? throw new Exception("Green Pars key not found.");
        public async Task SendOtpAsync(string otpCode, string mobile)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endPoint);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", $"basic apikey:{apiKey}");
            var payload = new
            {
                Mobile = mobile,
                SmsCode = otpCode,
                AddName = true
            };

            var json = JsonSerializer.Serialize(payload);

            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseAsString = response.Content.ReadAsStringAsync();
        }
    }
}
