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
    public class SubCategoriesController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SubCategoriesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        //[HasPermission(Permissions.SubCategories.Create)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSubCategoryCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        //[HasPermission(Permissions.SubCategories.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateSubCategoryCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { Id = id });
            return BaseOk();
        }

        //[HasPermission(Permissions.SubCategories.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteSubCategoryCommand(id));
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<SubCategoryDto>>> Get([FromQuery] GetSubCategoriesQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryDto>> GetCategory(Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetSubCategoryQuery(id));
            return OkOrNotFound(result);
        }

        [HttpGet("GetCategorySubCategories/{id}")]
        public async Task<ActionResult<PaginatedResult<SubCategoryDto>>> GetCategorySubCategories(Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetCategorySubCategoriesQuery(id));
            return OkOrNotFound(result);
        }
    }
}
