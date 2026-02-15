namespace ScrapDealer.Application.Services
{
    public interface ICaptchaService
    {
        (Guid CaptchaId, byte[] CaptchaImage) GenerateCaptcha(int length = 6);
        bool ValidateCaptcha(string captchaId, string captchaCode);
    }
}
