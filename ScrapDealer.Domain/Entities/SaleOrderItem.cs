using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.SaleOrders;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class SaleOrderItem : Entity<Guid>
    {
        public SubCategory? SubCategory { get; private set; }
        public Guid? SubCategoryId { get; private set; }
        public SaleType SaleType { get; private set; }
        public SaleOrderDescription? SellerDescription { get; private set; }
        public SaleOrderDescription? SystemDescription { get; private set; }
        public ICollection<Guid> Images { get; private set; } = [];

        public SaleOrderItem() { }

        public SaleOrderItem(ICollection<Guid> images, SubCategory? subCategory,
            SaleOrderDescription? systemDescription, SaleOrderDescription? sellerDescription, SaleType saleType)
        {
            Images = images;
            SubCategory = subCategory;
            SaleType = saleType;
            SystemDescription = systemDescription;
            SellerDescription = sellerDescription;
            SubCategoryId = subCategory?.Id;
        }

        internal void AdminUpdate(SubCategory? subCategory, SaleOrderDescription? systemDescription, SaleType saleType)
        {
            SubCategory = subCategory;
            SystemDescription = systemDescription;
            SaleType = saleType;
        }
    }
}
