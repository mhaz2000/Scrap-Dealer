using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record SaleOrderItemCommand(Guid? SubCategoryId, string? Description, SaleType? SaleType, ICollection<Guid> images);
}
