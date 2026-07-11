using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Claims;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{
    internal class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, AuthenticationDto>
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ITokenService _tokenService;
        private readonly IUserReadService _userReadService;
        private readonly IRoleReadService _roleReadService;
        private readonly IUserRepository _userRepository;

        public RefreshTokenHandler(
            IRedisCacheService redisCacheService,
            ITokenService tokenService,
            IUserReadService userReadService,
            IRoleReadService roleReadService,
            IUserRepository userRepository)
        {
            _redisCacheService = redisCacheService;
            _tokenService = tokenService;
            _userReadService = userReadService;
            _roleReadService = roleReadService;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                throw new BusinessException("توکن رفرش معتبر نمی‌باشد.");

            var userId = await _redisCacheService.GetAsync<Guid>($"refreshToken:{request.RefreshToken}");

            if (userId == Guid.Empty)
                throw new BusinessException("توکن رفرش نامعتبر یا منقضی شده است.");

            var user = await _userRepository.GetAsync(t=> t.Id == userId);
            if (user == null)
                throw new BusinessException("کاربر یافت نشد.");

            if (!(await _userReadService.CheckIfUserActiveAsync(userId)))
                throw new BusinessException("حساب کاربری شما غیر فعال شده است.");

            var userRoleName = await _roleReadService.GetUserRoleNameAsync(userId);
            if (string.IsNullOrEmpty(userRoleName))
                userRoleName = "User";

            var newTokens = _tokenService.GenerateToken(
                userId.ToString(),
                new List<Claim> { new("role", userRoleName) }
            );

            _redisCacheService.Remove($"refreshToken:{request.RefreshToken}");

            await _redisCacheService.SetAsync<Guid>(
                $"refreshToken:{newTokens.refreshToken}",
                userId,
                TimeSpan.FromDays(7)
            );

            // Commit changes
            await _userRepository.CommitAsync();

            return new AuthenticationDto
            {
                Token = newTokens.token,
                RefreshToken = newTokens.refreshToken
            };
        }
    }
}