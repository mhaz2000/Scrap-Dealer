using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Rewards;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Rewards;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddReward([FromBody] AddRewardCommand command)
        {
            await commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        //[HttpDelete("{id:guid}")]
        //public async Task<IActionResult> RemoveReward([FromRoute] Guid id)
        //{
        //    await commandDispatcher.DispatchAsync(new RemoveRewardCommand(id));
        //    return BaseOk();
        //}

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<RewardDto>>> GetAll([FromQuery] GetRewardsQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
    }
}
