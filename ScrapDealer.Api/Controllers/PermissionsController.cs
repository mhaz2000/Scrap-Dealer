using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.SystemPermissions;
using ScrapDealer.Application.Queries.Permissions;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(IQueryDispatcher _queryDispatcher, ICommandDispatcher _commandDispatcher) : BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<string>>> Get([FromQuery] GetPermissionsQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        //For now we have only one role which has permissions
        [Authorize(Roles = "Admin,Support")]
        [HttpGet("Support")]
        public async Task<ActionResult<PaginatedResult<string>>> Get([FromQuery] GetSupportPermissionsQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePermissionCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }
    }
}
