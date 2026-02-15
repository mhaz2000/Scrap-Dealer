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
        [HttpPost("OtpRequest")]
        public async Task<ActionResult<string>> OtpRequest([FromBody] OtpRequestCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpPost("OtpLogin")]
        public async Task<ActionResult<AuthenticationDto>> OtpLogin([FromBody] OtpLoginCommand command)
        {
            var response = await _commandDispatcher.DispatchAsync<OtpLoginCommand, AuthenticationDto>(command);
            return BaseObjectOk(response);
        }

        //[HasPermission(Permissions.Users.State)]
        [HttpGet("State")]
        public IActionResult State()
            => BaseOk("کاربر احراز هویت شده است.");
    }
}
