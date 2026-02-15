using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.SaleOrders
{
    public record GetSaleOrderQuery(Guid id) : IQuery<SaleOrderDto>;


}
