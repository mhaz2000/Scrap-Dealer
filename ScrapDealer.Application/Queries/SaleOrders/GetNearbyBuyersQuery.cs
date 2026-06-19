using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.SaleOrders
{
    public record GetNearbyBuyersQuery(Guid sellerId, Guid saleOrderId, int take = 10, int skip = 0) : IQuery<List<NearbyBuyerDto>>;
    public record GetSaleOrderRequestsQuery(Guid UserId, int take = 10, int skip = 0) : IQuery<List<SaleOrderRequestDto>>;
}
