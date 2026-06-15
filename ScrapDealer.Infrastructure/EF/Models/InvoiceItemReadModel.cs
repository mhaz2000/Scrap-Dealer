using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class InvoiceItemReadModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public SaleType SaleType { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategoryReadModel? SubCategory { get; set; }
        public InvoiceReadModel Invoice { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
