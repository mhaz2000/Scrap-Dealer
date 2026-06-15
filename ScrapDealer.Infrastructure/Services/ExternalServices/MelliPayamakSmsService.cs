using Microsoft.Extensions.Configuration;
using ScrapDealer.Application.Services.ExternalServices;

namespace ScrapDealer.Infrastructure.Services.ExternalServices
{
    public class MelliPayamakSmsService(HttpClient httpClient, IConfiguration configuration) : IMelliPayamakSmsService
    {
        private readonly string endPoint = configuration["MelliPayamak:Address"] ?? throw new Exception("Melli Payamak endpoint not found.");
        private readonly string apiKey = configuration["MelliPayamak:ApiKey"] ?? throw new Exception("Melli Payamak key not found.");
        private readonly string number = configuration["MelliPayamak:Number"] ?? throw new Exception("Melli Payamak number not found.");
        private readonly string username = configuration["MelliPayamak:UserName"] ?? throw new Exception("Melli Payamak username not found.");

        public async Task SendOtpAsync(string otpCode, string mobile)
        {
            string text = $"رمز یکبار مصرف شما: {otpCode}\n\nضایعات چی";
            var formData = new Dictionary<string, string>
            {
                { "username", username },   // or a dedicated username config value
                { "password", apiKey },   // adjust based on MelliPayamak's auth scheme
                { "to", mobile },
                { "from", number },
                { "text", text },
                { "isflash", "false" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, endPoint)
            {
                Content = new FormUrlEncodedContent(formData)
            };

            request.Headers.Add("cache-control", "no-cache");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
