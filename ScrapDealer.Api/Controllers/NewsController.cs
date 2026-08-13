using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.NewsCommand;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.NewsQueries;
using ScrapDealer.Infrastructure;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using ScrapDealer.Shared.SystemPermissions;

namespace ScrapDealer.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public NewsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        
        //[HasPermission(Permissions.News.Create)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNewsCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        
        //[HasPermission(Permissions.News.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateNewsCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { id = id });
            return BaseOk();
        }

        
        //[HasPermission(Permissions.News.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteNewsCommand(id));
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<NewsDto>>> Get([FromQuery] GetNewsListQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNew(Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetSingleNewsQuery(id));
            return OkOrNotFound(result);
        }
    }
}
