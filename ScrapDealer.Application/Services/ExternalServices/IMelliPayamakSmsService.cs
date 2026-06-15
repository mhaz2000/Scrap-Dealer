namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface IMelliPayamakSmsService
    {
        Task SendOtpAsync(string otpCode, string mobile);
    }
}
