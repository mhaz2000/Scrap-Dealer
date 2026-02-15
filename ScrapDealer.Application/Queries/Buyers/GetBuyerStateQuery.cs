using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Buyers
{
    public record GetBuyerStateQuery(Guid UserId) : IQuery<bool>;
}
