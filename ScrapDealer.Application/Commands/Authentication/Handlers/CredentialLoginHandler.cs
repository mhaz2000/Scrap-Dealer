using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Claims;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{


    public class CredentialLoginHandler : ICommandHandler<CredentialLoginCommand, PanelAuthenticationDto>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserReadService _readService;
        private readonly IRoleReadService _roleReadService;
        private readonly ICaptchaService _captchaService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IRolePermissionService _rolePermissionService;

        public CredentialLoginHandler(ITokenService tokenService, IUserReadService readService, IRedisCacheService redisCacheService,
            ICaptchaService captchaService, IRoleReadService roleReadService, IRolePermissionService rolePermissionService)
        {
            _readService = readService;
            _tokenService = tokenService;
            _captchaService = captchaService;
            _roleReadService = roleReadService;
            _rolePermissionService = rolePermissionService;
            _redisCacheService = redisCacheService;
        }

        public async Task<PanelAuthenticationDto> Handle(CredentialLoginCommand command, CancellationToken cancellationToken)
        {
            //var (Username, Password, CaptchaCode, CaptchaId) = command;
            var (Username, Password) = command;

            //if (!_captchaService.ValidateCaptcha(CaptchaId.ToString(), CaptchaCode))
            //    throw new BusinessException("کپچا صحیح نمی‌باشد.");

            var userId = await _readService.ValidateUserCredentialByUsernameAsync(Username, Password);
            if (userId is null)
                throw new BusinessException("نام کاربری یا رمز عبور اشتباه می‌باشد.");

            var userRoleName = await _roleReadService.GetUserRoleNameAsync(userId.Value);
            if (string.IsNullOrEmpty(userRoleName))
                throw new BusinessException("نقش کاربر یافت نشد.");

            IEnumerable<string> permissions = Enumerable.Empty<string>();
            if (userRoleName == "Support")
                permissions = await _rolePermissionService.GetRolePermissionsAsync("Support");

            var tokens = _tokenService.GenerateToken(
                           userId.Value.ToString(),
                           new List<Claim> { new("role", userRoleName) }
                       );

            // Store refresh token in Redis
            await _redisCacheService.SetAsync<Guid>(
                $"adminRefreshToken:{tokens.refreshToken}",
                userId.Value,
                TimeSpan.FromDays(7)
            );


            return new PanelAuthenticationDto()
            {
                Token = tokens.token,
                RefreshToken = tokens.refreshToken,
                Role = userRoleName,
                Permissions = permissions
            };
        }
    }
}
