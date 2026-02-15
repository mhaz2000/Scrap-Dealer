using ScrapDealer.Application.DTO;
using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record ReviewSaleOrderItemCommand
    {
        public string? Description { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SaleType SaleType { get; set; }
        public Guid Id { get; set; }
    }
}
