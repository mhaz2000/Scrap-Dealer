using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISaleOrderFactory
    {
        SaleOrder Create(bool isIndustrial, Seller seller, SaleOrderAddress address, double? latitude, double? longitude, bool saleAtBuyersLocation, Telephone? telephone);
        SaleOrder Update(SaleOrderAddress address, double latitude, double longitude, Telephone? telephone, bool saleAtBuyersLocation, SaleOrder saleOrder);

        SaleOrderItem CreateItem(ICollection<Guid> images, SubCategory? subCategory,
            Description? systemDescription, Description? sellerDescription, SaleType saleType);

        SaleOrderItem UpdateItem(SubCategory? subCategory, Description? systemDescription, SaleType saleType, SaleOrderItem item, SaleOrder saleOrder);
    }
}
