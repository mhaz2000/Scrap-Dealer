using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Tickets
{
    public record GetTicketQuery(Guid Id) : IQuery<TicketDetailDto>;
}
