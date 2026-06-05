using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Tickets
{
    public record GetTicketsQuery(Guid UserId) : PaginationQuery, IQuery<PaginatedResult<TicketDto>>;
}
