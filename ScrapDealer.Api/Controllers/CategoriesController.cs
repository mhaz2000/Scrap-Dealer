using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Categories;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Categories;
using ScrapDealer.Infrastructure;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using ScrapDealer.Shared.SystemPermissions;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CategoriesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        
        //[HasPermission(Permissions.Categories.Create)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCategoryCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        
        //[HasPermission(Permissions.Categories.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { Id = id});
            return BaseOk();
        }

        
        //[HasPermission(Permissions.Categories.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteCategoryCommand(id));
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<CategoryDto>>> Get([FromQuery] GetCategoriesQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetCategoryQuery(id));
            return OkOrNotFound(result);
        }
    }
}
