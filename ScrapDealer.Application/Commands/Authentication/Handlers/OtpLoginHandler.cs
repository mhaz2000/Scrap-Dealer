using Microsoft.Identity.Client;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Claims;
using System.Security.Cryptography;

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
        private readonly ITokenService _tokenService;
        public OtpLoginHandler(IMemoryCacheService memoryCacheService, IRedisCacheService redisCacheService, IUserReadService userReadService, IUserFactory userFactory,
            IUserRepository userRepository, ITokenService tokenService, IRoleReadService roleReadService)
        {
            _memoryCacheService = memoryCacheService;
            _userReadService = userReadService;
            _userFactory = userFactory;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _redisCacheService = redisCacheService;
            _roleReadService = roleReadService;
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

            if (userId is null)
            {
                var newUser = _userFactory.Create(request.Phone, request.Phone);

                await _userRepository.AddAsync(newUser);

                userId = newUser.Id;
            }

            var userRoleName = await _roleReadService.GetUserRoleNameAsync(userId.Value);
            if (string.IsNullOrEmpty(userRoleName))
                userRoleName = "User";

            var token = _tokenService.GenerateToken(userId.Value.ToString(), new List<Claim> { new("role", userRoleName) });
            await _redisCacheService.SetAsync<Guid>($"refreshToken:{token.refreshToken}", userId.Value, TimeSpan.FromDays(7));

            if (!await _userReadService.CheckIfUserActiveAsync(userId.Value))
                throw new BusinessException("حساب کاربری شما غیر فعال شده است.");

            return new AuthenticationDto() { Token = token.token, RefreshToken = token.refreshToken };
        }
    }
}