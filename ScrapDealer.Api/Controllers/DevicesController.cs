using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Devices;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController(ICommandDispatcher commandDispatcher) : BaseController
    {
        [HttpPost("Register")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterDeviceCommand command)
        {
            await commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }

        [HttpPost("Unregister")]
        [Authorize]
        public async Task<IActionResult> Unregister([FromBody] UnregisterDeviceCommand command)
        {
            await commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }
    }
}