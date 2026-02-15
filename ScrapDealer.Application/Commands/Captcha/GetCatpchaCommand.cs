using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Captcha
{
    public record GetCatpchaCommand : ICommand<CaptchaDto>;

}
