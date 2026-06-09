using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.SaleOrders;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Infrastructure;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;
using ScrapDealer.Shared.SystemPermissions;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrdersController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public SaleOrdersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSaleOrderCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateSaleOrderCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { Id = id });
            return BaseOk();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteSaleOrderCommand(id));
            return BaseOk();
        }

        //[HasPermission(Permissions.SaleOrders.ViewAll)]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<SaleOrderDto>>> Get([FromQuery] GetSaleOrdersQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet("MySaleOrder")]
        public async Task<ActionResult<PaginatedResult<SaleOrderDto>>> GetMySaleOrder()
        {
            var result = await _queryDispatcher.QueryAsync(new GetMySaleOrdersQuery(UserId));
            return OkOrNotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleOrderDto>> GetSaleOrder(Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetSaleOrderQuery(id));
            return OkOrNotFound(result);
        }

        //[HasPermission(Permissions.SaleOrders.Review)]
        [HttpPut("Review/{id:guid}")]
        public async Task<IActionResult> Review([FromRoute] Guid id, [FromBody] ReviewSaleOrderCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { Id = id});
            return BaseOk();
        }

        //[HasPermission(Permissions.SaleOrders.Verification)]
        [HttpPut("Verification/{id:guid}/{verificationStatus:bool}")]
        public async Task<IActionResult> Verification([FromRoute] Guid id, bool verificationStatus, [FromBody] string? rejectionReason)
        {
            await _commandDispatcher.DispatchAsync(new SaleOrderVerificationCommand(id, verificationStatus, rejectionReason));
            return BaseOk();
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("NearbyOrdersForBuyers")]
        public async Task<ActionResult<List<SaleOrderDto>>> GetNearbyOrdersForBuyers()
        {
            var result = await _queryDispatcher.QueryAsync(new GetNearbyOrdersForBuyersQuery(UserId));
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("GetNearbyBuyers/{saleOrderId:guid}")]
        public async Task<ActionResult<List<NearbyBuyerDto>>> GetNearbyBuyers([FromRoute] Guid saleOrderId)
        {
            var result = await _queryDispatcher.QueryAsync(new GetNearbyBuyersQuery(UserId, saleOrderId));
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Buyer")]
        [HttpPut("AcceptOrder/{id:guid}")]
        public async Task<IActionResult> AcceptOrder([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new AcceptOrderCommand(id, UserId));
            return BaseOk();
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("SendOrder")]
        public async Task<IActionResult> SendOrder([FromBody] SendOrderCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }
    }
}
