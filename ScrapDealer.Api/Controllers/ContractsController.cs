using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Contracts;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Contracts;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : BaseController
    {
        [Authorize(Roles = "Buyer")]
        [HttpGet("BuyerContracts")]
        public async Task<ActionResult<PaginatedResult<BuyerContractDto>>> GetBuyerContracts([FromQuery] GetBuyerContractsQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("SellerContracts")]
        public async Task<ActionResult<PaginatedResult<SellerContractDto>>> GetSellerContracts([FromQuery] GetSellerContractsQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("BuyerContract/{id:guid}")]
        public async Task<ActionResult<BuyerContractDetailDto>> GetBuyerContract([FromRoute] Guid id)
        {
            var result = await queryDispatcher.QueryAsync(new GetBuyerContractQuery(id));
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("SellerContract/{id:guid}")]
        public async Task<ActionResult<SellerContractDetailDto>> GetSellerContract([FromRoute] Guid id)
        {
            var result = await queryDispatcher.QueryAsync(new GetSellerContractQuery(id));
            return OkOrNotFound(result);
        }

        [Authorize(Roles ="Seller,Buyer")]
        [HttpPut]
        public async Task<IActionResult> CancelContract()
        {
            await commandDispatcher.DispatchAsync(new CancelContractCommand(UserId));
            return BaseOk();
        }
    }
}