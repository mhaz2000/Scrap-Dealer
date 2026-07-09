using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Claims;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{
    public class AdminRefreshTokenHandler : ICommandHandler<AdminRefreshTokenCommand, PanelAuthenticationDto>
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ITokenService _tokenService;
        private readonly IUserReadService _userReadService;
        private readonly IRoleReadService _roleReadService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IUserRepository _userRepository;

        public AdminRefreshTokenHandler(
            IRedisCacheService redisCacheService,
            ITokenService tokenService,
            IUserReadService userReadService,
            IRoleReadService roleReadService,
            IRolePermissionService rolePermissionService,
            IUserRepository userRepository)
        {
            _redisCacheService = redisCacheService;
            _tokenService = tokenService;
            _userReadService = userReadService;
            _roleReadService = roleReadService;
            _rolePermissionService = rolePermissionService;
            _userRepository = userRepository;
        }

        public async Task<PanelAuthenticationDto> Handle(AdminRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.RefreshToken))
                throw new BusinessException("توکن رفرش معتبر نمی‌باشد.");

            var userId = await _redisCacheService.GetAsync<Guid>($"adminRefreshToken:{command.RefreshToken}");

            if (userId == Guid.Empty)
                throw new BusinessException("توکن رفرش نامعتبر یا منقضی شده است.");

            var user = await _userRepository.GetAsync(t=> t.Id == userId);
            if (user == null)
                throw new BusinessException("کاربر یافت نشد.");

            if (!(await _userReadService.CheckIfUserActiveAsync(userId)))
                throw new BusinessException("حساب کاربری شما غیر فعال شده است.");

            var userRoleName = await _roleReadService.GetUserRoleNameAsync(userId);
            if (string.IsNullOrEmpty(userRoleName))
                throw new BusinessException("نقش کاربر یافت نشد.");

            IEnumerable<string> permissions = Enumerable.Empty<string>();
            if (userRoleName == "Support")
                permissions = await _rolePermissionService.GetRolePermissionsAsync("Support");

            var newTokens = _tokenService.GenerateToken(
                userId.ToString(),
                new List<Claim> { new("role", userRoleName) }
            );

            _redisCacheService.Remove($"adminRefreshToken:{command.RefreshToken}");

            await _redisCacheService.SetAsync<Guid>(
                $"adminRefreshToken:{newTokens.refreshToken}",
                userId,
                TimeSpan.FromDays(7)
            );

            await _userRepository.CommitAsync();

            return new PanelAuthenticationDto()
            {
                Token = newTokens.token,
                RefreshToken = newTokens.refreshToken, // Make sure PanelAuthenticationDto has RefreshToken property
                Role = userRoleName,
                Permissions = permissions
            };
        }
    }
}
