using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Tickets;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Application.Queries.Tickets;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTicket([FromBody] CreateTicketCommand command)
        {
            var ticketId = await commandDispatcher.DispatchAsync<CreateTicketCommand, Guid>(command with { UserId = UserId });
            return BaseObjectOk(ticketId);
        }

        [HttpPut]
        public async Task<IActionResult> AddMessage([FromBody] AddMessageCommand command)
        {
            await commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<TicketDto>>> GetTickets([FromQuery] GetTicketsQuery query)
        {
            var result = await queryDispatcher.QueryAsync(query with { UserId = UserId});
            return OkOrNotFound(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TicketDetailDto>> GetTicket([FromRoute] Guid id)
        {
            var result = await queryDispatcher.QueryAsync(new GetTicketQuery(id));
            return OkOrNotFound(result);
        }

        [HttpPut("Close/{id:guid}")]
        public async Task<IActionResult> Close([FromRoute] Guid id)
        {
            await commandDispatcher.DispatchAsync(new CloseTicketCommand(id));
            return BaseOk();
        }
    }
}