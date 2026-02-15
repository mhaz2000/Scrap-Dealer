using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Tickets
{
    public record GetTicketsQuery : PaginationQuery, IQuery<PaginatedResult<TicketDto>>;
}
