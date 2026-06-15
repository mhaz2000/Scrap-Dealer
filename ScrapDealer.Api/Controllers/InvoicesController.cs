using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Invoices;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Invoices;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : BaseController
    {
        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            await commandDispatcher.DispatchAsync(command with { UserId = UserId});
            return BaseOk("فاکتور با موفقیت ایجاد شد.");
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<InvoiceListDto>>> Get([FromQuery] GetInvoicesQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("MyInvoices")]
        public async Task<ActionResult<PaginatedResult<InvoiceListDto>>> GetMyInvoices([FromQuery] GetMyInvoicesQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query with { UserId = UserId});
            return OkOrNotFound(result);
        }

        [HttpGet("Detail/{id:guid}")]
        public async Task<ActionResult<InvoiceDto>> GetDetialById([FromRoute] Guid id)
        {
            var result = await queryDispatcher.QueryAsync(new GetInvoiceByIdQuery(id));
            return OkOrNotFound(result);
        }

        [HttpGet("GetByContract/{id:guid}")]
        public async Task<ActionResult<InvoiceDto>> GetDetialByContractId([FromRoute] Guid id)
        {
            var result = await queryDispatcher.QueryAsync(new GetInvoiceByIdQuery(id));
            return OkOrNotFound(result);
        }
    }
}