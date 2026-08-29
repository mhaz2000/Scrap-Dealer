using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.SaleOrders
{
    public record GetSaleOrderRequestsQuery(Guid UserId) : PaginationQuery, IQuery<PaginatedResult<SaleOrderRequestDto>>;
}
