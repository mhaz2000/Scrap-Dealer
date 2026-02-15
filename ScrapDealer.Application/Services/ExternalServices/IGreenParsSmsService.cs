namespace ScrapDealer.Application.Services.ExternalServices
{
    public interface IGreenParsSmsService
    {
        Task SendOtpAsync(string otpCode, string mobile);
    }
}
