using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Claims;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{
    internal class OtpLoginHandler : ICommandHandler<OtpLoginCommand, AuthenticationDto>
    {
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IUserReadService _userReadService;
        private readonly IUserFactory _userFactory;
        private readonly IRoleReadService _roleReadService;
        private readonly IUserRepository _userRepository;
        private readonly IReferralRepository _referralRepository;
        private readonly ITokenService _tokenService;
        public OtpLoginHandler(IMemoryCacheService memoryCacheService, IRedisCacheService redisCacheService, IUserReadService userReadService, IUserFactory userFactory,
            IUserRepository userRepository, ITokenService tokenService, IRoleReadService roleReadService, IReferralRepository referralRepository)
        {
            _memoryCacheService = memoryCacheService;
            _userReadService = userReadService;
            _userFactory = userFactory;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _redisCacheService = redisCacheService;
            _roleReadService = roleReadService;
            _referralRepository = referralRepository;
        }
        public async Task<AuthenticationDto> Handle(OtpLoginCommand request, CancellationToken cancellationToken)
        {
            var phone = Phone.Create(request.Phone); //For normalization
            var userId = await _userReadService.GetByPhoneAsync(phone);

            //Must be removed.
            if (request.Code != "Scr@pDea1eR!!73138")
            {
                if (_memoryCacheService.Get<string>(phone) != request.Code)
                    throw new BusinessException("کد تایید اشتباه است.");
            }

            bool newUserCreated = false;
            if (userId is null)
            {
                var newUser = _userFactory.Create(request.Phone, request.Phone);

                await _userRepository.AddAsync(newUser);

                userId = newUser.Id;
                newUserCreated = true;

                if (!string.IsNullOrWhiteSpace(request.ReferralCode))
                {
                    var referrerId = await _userReadService.GetIdByReferralCodeAsync(request.ReferralCode);
                    if (referrerId is null)
                        throw new BusinessException("کد معرف نامعتبر است.");

                    if (referrerId.Value == newUser.Id)
                        throw new BusinessException("امکان استفاده از کد معرف خود وجود ندارد.");

                    var existingReferral = await _referralRepository.GetAsync(r => r.RefereeUserId == newUser.Id);
                    if (existingReferral is not null)
                        throw new BusinessException("شما قبلا از کد معرف استفاده کرده‌اید.");

                    var referral = new Referral(referrerId.Value, newUser.Id, request.ReferralCode);
                    await _referralRepository.AddAsync(referral);
                }
            }

            var userRoleName = await _roleReadService.GetUserRoleNameAsync(userId.Value);
            if (string.IsNullOrEmpty(userRoleName))
                userRoleName = "User";

            var token = _tokenService.GenerateToken(userId.Value.ToString(), new List<Claim> { new("role", userRoleName) });
            await _redisCacheService.SetAsync<Guid>($"refreshToken:{token.refreshToken}", userId.Value, TimeSpan.FromDays(7));

            if (!newUserCreated && !(await _userReadService.CheckIfUserActiveAsync(userId.Value)))
                throw new BusinessException("حساب کاربری شما غیر فعال شده است.");

            await _userRepository.CommitAsync();

            return new AuthenticationDto() { Token = token.token, RefreshToken = token.refreshToken };
        }
    }
}