using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Sellers
{
    public record GetSellersQuery(bool? Verified) : PaginationQuery, IQuery<PaginatedResult<SellerProfileDto>>;

}
