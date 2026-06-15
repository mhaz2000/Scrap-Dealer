using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class InvoiceItem : Entity<Guid>
    {
        public Amount Amount { get; set; }

        public SubCategory? SubCategory { get; private set; }
        public Guid? SubCategoryId { get; private set; }
        public SaleType SaleType { get; private set; }

        private InvoiceItem()
        {
            
        }

        public InvoiceItem(Amount amount, SubCategory subCategory, SaleType saleType)
        {
            Amount = amount;
            SubCategory = subCategory;
            SaleType = saleType;
        }
    }
}
