using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories
{
    public class SaleOrderFactory : ISaleOrderFactory
    {
        public SaleOrder Create(bool isIndustrial, Seller seller, SaleOrderAddress address, double? latitude, double? longitude,
            bool saleAtBuyersLocation, Telephone? telephone, Code code)
        {
            var telephoneValue = string.IsNullOrEmpty(telephone) ? null: Telephone.Create(telephone);
            var addressValue = SaleOrderAddress.Create(address);
            var locationValue = latitude is null || longitude is null ? null : Location.Create(latitude.Value, longitude.Value);

            return new SaleOrder(isIndustrial, seller, addressValue, locationValue, telephoneValue, saleAtBuyersLocation, code);
        }

        public SaleOrderItem CreateItem(ICollection<Guid> images, SubCategory? subCategory,
            Description? systemDescription, Description? sellerDescription, SaleType? saleType)
        {
            var sellerDescriptionValue = Description.Create(sellerDescription);
            var systemDescriptionValue = Description.Create(systemDescription);

            return new SaleOrderItem(images, subCategory, systemDescription, sellerDescription, saleType);
        }

        public SaleOrder Update(SaleOrderAddress address, double latitude, double longitude, Telephone? telephone, bool saleAtBuyersLocation, SaleOrder saleOrder)
        {
            var telephoneValue = string.IsNullOrEmpty(telephone) ? null: Telephone.Create(telephone);
            var addressValue = SaleOrderAddress.Create(address);
            var locationValue = Location.Create(latitude, longitude);

            saleOrder.Update(addressValue, locationValue, telephoneValue, saleAtBuyersLocation);

            return saleOrder;

        }

        public SaleOrderItem UpdateItem(SubCategory? subCategory, Description? systemDescription, SaleType? saleType, SaleOrderItem item, SaleOrder saleOrder)
        {
            var systemDescriptionValue = Description.Create(systemDescription);

            saleOrder.SetAsUpdated();
            item.AdminUpdate(subCategory, systemDescriptionValue, saleType);
            return item;
        }
    }
}
