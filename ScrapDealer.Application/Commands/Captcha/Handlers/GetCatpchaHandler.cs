using MediatR;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Application.Commands.Captcha;

namespace ScrapDealer.Application.Commands.Captcha.Handlers
{
    internal class GetCatpchaHandler : ICommandHandler<GetCatpchaCommand, CaptchaDto>
    {
        private readonly ICaptchaService _captchaService;

        public GetCatpchaHandler(ICaptchaService captchaService)
            => _captchaService = captchaService;

        public Task<CaptchaDto> Handle(GetCatpchaCommand request, CancellationToken cancellationToken)
        {
            var (captchaId, captchaImage) = _captchaService.GenerateCaptcha();

            return Task.FromResult(new CaptchaDto(captchaId, Convert.ToBase64String(captchaImage)));
        }
    }
}
