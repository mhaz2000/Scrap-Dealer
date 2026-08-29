using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.SaleOrders
{
    public record GetNearbyOrdersForBuyersQuery(Guid buyerId, double distance) : PaginationQuery, IQuery<PaginatedResult<SaleOrderDto>>;
}
