using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Sellers
{
    public record GetSellerProfileQuery(Guid UserId) : IQuery<SellerProfileDto>;
}
