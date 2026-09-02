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
            var result = await queryDispatcher.QueryAsync(new GetInvoiceByContractIdQuery(id));
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("PendingApproved")]
        public async Task<ActionResult<PaginatedResult<InvoiceListDto>>> GetPendingApproved([FromQuery] GetMyPendingInvoicesQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query with { UserId = UserId });
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("{id:guid}/approve")]
        public async Task<IActionResult> ApproveInvoice(Guid id)
        {
            await commandDispatcher.DispatchAsync(new ApproveInvoiceCommand(id, UserId));
            return BaseOk("فاکتور تأیید شد.");
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("{id:guid}/reject")]
        public async Task<IActionResult> RejectInvoice(Guid id)
        {
            await commandDispatcher.DispatchAsync(new RejectInvoiceCommand(id, UserId, null));
            return BaseOk("فاکتور رد شد.");
        }

        [Authorize(Roles = "Buyer")]
        [HttpPut("{id:guid}/edit")]
        public async Task<IActionResult> EditInvoice(Guid id, [FromBody] EditInvoiceCommand command)
        {
            await commandDispatcher.DispatchAsync(command with { InvoiceId = id, UserId = UserId });
            return BaseOk("فاکتور ویرایش شد و مجدداً برای بررسی ارسال شد.");
        }
    }
}