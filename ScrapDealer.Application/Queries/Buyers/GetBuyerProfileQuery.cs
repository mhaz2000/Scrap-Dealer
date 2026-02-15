using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Buyers
{
    public record GetBuyerProfileQuery(Guid UserId) : IQuery<BuyerProfileDto>;

}
