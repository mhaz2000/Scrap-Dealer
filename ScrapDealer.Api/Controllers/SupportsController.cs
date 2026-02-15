using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Supports;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Supports;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController(ICommandDispatcher _commandDispatcher, IQueryDispatcher _queryDispatcher) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSupportCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupportDto>> Get([FromRoute] Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetSupportQuery(id));
            return OkOrNotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<SupportDto>>> Get([FromQuery] GetSupportsQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateSupportCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { Id = id});
            return BaseOk();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteSupportCommand(id));
            return BaseOk();
        }
    }
}
