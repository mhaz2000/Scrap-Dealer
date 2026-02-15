using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.SystemSettings;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SystemSettings;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSetting([FromBody] UpdateSettingsCommand command)
        {
            await commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<SettingsDto>> GetSettings()
        {
            var result = await queryDispatcher.QueryAsync(new GetSettingsQuery());
            return OkOrNotFound(result);
        }
    }
}
