using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class SaleOrderItem : Entity<Guid>
    {
        public SubCategory? SubCategory { get; private set; }
        public Guid? SubCategoryId { get; private set; }
        public SaleType? SaleType { get; private set; }
        public Description? SellerDescription { get; private set; }
        public Description? SystemDescription { get; private set; }
        public ICollection<Guid> Images { get; private set; } = [];
        public bool ModifiedByAdmin { get; set; } = false;

        public SaleOrderItem() { }

        public SaleOrderItem(ICollection<Guid> images, SubCategory? subCategory,
            Description? systemDescription, Description? sellerDescription, SaleType? saleType)
        {
            Images = images;
            SubCategory = subCategory;
            SaleType = saleType;
            SystemDescription = systemDescription;
            SellerDescription = sellerDescription;
            SubCategoryId = subCategory?.Id;
        }

        internal bool AdminUpdate(SubCategory? subCategory, Description? systemDescription, SaleType? saleType)
        {
            var changed = !Equals(SubCategoryId, subCategory?.Id)
                || !Equals(SystemDescription?.Value, systemDescription?.Value)
                || !Equals(SaleType, saleType);

            if (changed)
            {
                ModifiedByAdmin = true;
                SubCategory = subCategory;
                SystemDescription = systemDescription;
                SaleType = saleType;
                SubCategoryId = subCategory?.Id;
            }

            return changed;
        }
    }
}
