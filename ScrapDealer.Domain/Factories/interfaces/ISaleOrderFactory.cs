using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISaleOrderFactory
    {
        SaleOrder Create(bool isIndustrial, Seller seller, SaleOrderAddress address, double latitude, double longitude, Telephone? telephone);
        SaleOrder Update(SaleOrderAddress address, double latitude, double longitude, Telephone? telephone, SaleOrder saleOrder);

        SaleOrderItem CreateItem(ICollection<Guid> images, SubCategory? subCategory,
            SaleOrderDescription? systemDescription, SaleOrderDescription? sellerDescription, SaleType saleType);

        SaleOrderItem UpdateItem(SubCategory? subCategory, SaleOrderDescription? systemDescription, SaleType saleType, SaleOrderItem item, SaleOrder saleOrder);
    }
}
