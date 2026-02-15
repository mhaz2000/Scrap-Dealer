using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Buyers;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public BuyersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        //[HasPermission(Permissions.Buyers.State)]
        [HttpGet("State")]
        public async Task<ActionResult<bool>> State()
        {
            var result = await _queryDispatcher.QueryAsync(new GetBuyerStateQuery(UserId));
            return OkOrNotFound(result);
        }

        [HttpGet("Profile")]
        public async Task<ActionResult<BuyerProfileDto>> GetProfile()
        {
            var result = await _queryDispatcher.QueryAsync(new GetBuyerProfileQuery(UserId));
            return OkOrNotFound(result);
        }

        //[HasPermission(Permissions.Buyers.ViewAll)]
        [HttpGet("Admin/Get")]
        public async Task<ActionResult<PaginatedResult<BuyerProfileDto>>> Get([FromQuery] GetBuyersQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBuyerCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }

        //[HasPermission(Permissions.Buyers.Verify)]
        [HttpPut("Admin/Verify/{id}")]
        public async Task<IActionResult> Verfiy(Guid id)
        {
            await _commandDispatcher.DispatchAsync(new VerifyBuyerCommand(id));
            return BaseOk();
        }

        //[HasPermission(Permissions.Buyers.ToggleActivation)]
        [HttpPut("Admin/Activation/{id}/{status}")]
        public async Task<IActionResult> ToggleActivation(Guid id, bool status)
        {
            await _commandDispatcher.DispatchAsync(new BuyerToggleActivationCommand(id, status));
            return BaseOk();
        }
    }
}