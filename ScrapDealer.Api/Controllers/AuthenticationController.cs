using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Authentication;
using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AuthenticationController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [AllowAnonymous]
        [HttpPost("CredentialLogin")]
        public async Task<ActionResult<PanelAuthenticationDto>> Post([FromBody] CredentialLoginCommand command)
        {
            var response = await _commandDispatcher.DispatchAsync<CredentialLoginCommand, PanelAuthenticationDto>(command);
            return OkOrNotFound(response);
        }

        [AllowAnonymous]
        [HttpPost("Admin/RefreshToken")]
        public async Task<ActionResult<PanelAuthenticationDto>> AdminRefreshToken([FromBody] AdminRefreshTokenCommand command)
        {
            var response = await _commandDispatcher.DispatchAsync<AdminRefreshTokenCommand, PanelAuthenticationDto>(command);
            return OkOrNotFound(response);
        }

        [AllowAnonymous]
        [HttpPost("OtpRequest")]
        public async Task<ActionResult<bool>> OtpRequest([FromBody] OtpRequestCommand command)
        {
            var isNewUser = await _commandDispatcher.DispatchAsync<OtpRequestCommand, bool>(command);
            return BaseObjectOk( isNewUser);
        }

        [AllowAnonymous]
        [HttpPost("OtpLogin")]
        public async Task<ActionResult<AuthenticationDto>> OtpLogin([FromBody] OtpLoginCommand command)
        {
            var response = await _commandDispatcher.DispatchAsync<OtpLoginCommand, AuthenticationDto>(command);
            return BaseObjectOk(response);
        }

        [AllowAnonymous]
        [HttpPost("User/RefreshToken")]
        public async Task<ActionResult<AuthenticationDto>> UserRefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _commandDispatcher.DispatchAsync<RefreshTokenCommand, AuthenticationDto>(command);
            return OkOrNotFound(response);
        }

        //[HasPermission(Permissions.Users.State)]
        [HttpGet("State")]
        public IActionResult State()
            => BaseOk("کاربر احراز هویت شده است.");
    }
}
