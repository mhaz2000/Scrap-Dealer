using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories
{
    public class SaleOrderFactory : ISaleOrderFactory
    {
        public SaleOrder Create(bool isIndustrial, Seller seller, SaleOrderAddress address, double latitude, double longitude, Telephone? telephone)
        {
            var telephoneValue = string.IsNullOrEmpty(telephone) ? null: Telephone.Create(telephone);
            var addressValue = SaleOrderAddress.Create(address);
            var locationValue = Location.Create(latitude, longitude);

            return new SaleOrder(isIndustrial, seller, addressValue, locationValue, telephoneValue);
        }

        public SaleOrderItem CreateItem(ICollection<Guid> images, SubCategory? subCategory,
            SaleOrderDescription? systemDescription, SaleOrderDescription? sellerDescription, SaleType saleType)
        {
            var sellerDescriptionValue = SaleOrderDescription.Create(sellerDescription);
            var systemDescriptionValue = SaleOrderDescription.Create(systemDescription);

            return new SaleOrderItem(images, subCategory, systemDescription, sellerDescription, saleType);
        }

        public SaleOrder Update(SaleOrderAddress address, double latitude, double longitude, Telephone? telephone, SaleOrder saleOrder)
        {
            var telephoneValue = string.IsNullOrEmpty(telephone) ? null: Telephone.Create(telephone);
            var addressValue = SaleOrderAddress.Create(address);
            var locationValue = Location.Create(latitude, longitude);

            saleOrder.Update(addressValue, locationValue, telephoneValue);

            return saleOrder;

        }

        public SaleOrderItem UpdateItem(SubCategory? subCategory, SaleOrderDescription? systemDescription, SaleType saleType, SaleOrderItem item, SaleOrder saleOrder)
        {
            var systemDescriptionValue = SaleOrderDescription.Create(systemDescription);

            saleOrder.SetAsUpdated();
            item.AdminUpdate(subCategory, systemDescriptionValue, saleType);
            return item;
        }
    }
}
