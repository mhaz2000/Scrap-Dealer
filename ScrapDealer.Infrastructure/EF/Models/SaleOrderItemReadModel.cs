using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SaleOrderItemReadModel
    {
        public Guid Id { get; set; }
        public SaleType SaleType { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategoryReadModel? SubCategory { get; set; }
        public ICollection<Guid> Images { get; set; } = [];
        public string? SellerDescription { get; set; }
        public string? SystemDescription { get; set; }
        public SaleOrderReadModel SaleOrder { get; set; }
        public Guid SaleOrderId { get; set; }
    }
}
