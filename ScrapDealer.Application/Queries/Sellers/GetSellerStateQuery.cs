using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Sellers
{
    public record GetSellerStateQuery(Guid UserId) : IQuery<bool>;

}
