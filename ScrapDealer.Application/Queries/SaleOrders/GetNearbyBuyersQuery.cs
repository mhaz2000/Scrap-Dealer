using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.SaleOrders
{
    public record GetNearbyBuyersQuery(Guid sellerId, Guid saleOrderId, double distance) : PaginationQuery, IQuery<PaginatedResult<NearbyBuyerDto>>;
}
