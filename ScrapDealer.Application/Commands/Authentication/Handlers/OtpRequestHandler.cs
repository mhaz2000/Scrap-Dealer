using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{
    public class OtpRequestHandler : ICommandHandler<OtpRequestCommand, bool>
    {
        private readonly IMemoryCacheService _cacheService;
        private readonly IGreenParsSmsService _greenParsSmsService;
        private readonly IMelliPayamakSmsService _melliPayamakSmsService;
        private readonly IUserReadService _userReadService;

        public OtpRequestHandler(IMemoryCacheService cacheService, IGreenParsSmsService greenParsSmsService, IUserReadService userReadService, IMelliPayamakSmsService melliPayamakSmsService)
        {
            _cacheService = cacheService;
            _greenParsSmsService = greenParsSmsService;
            _melliPayamakSmsService = melliPayamakSmsService;
            _userReadService = userReadService;
        }
        public async Task<bool> Handle(OtpRequestCommand request, CancellationToken cancellationToken)
        {
            var random = new Random();
            var phone = Phone.Create(request.Phone); //For normalization

            var otpCode = random.Next(100000, 999999);
            _cacheService.Set(phone, otpCode.ToString(), TimeSpan.FromMinutes(2));

            //Sms
            //await _greenParsSmsService.SendOtpAsync(otpCode.ToString(), phone);
            await _melliPayamakSmsService.SendOtpAsync(otpCode.ToString(), phone);

            if (await _userReadService.ExistsByPhoneAsync(phone))
                return false;

            return true;
        }
    }
}
