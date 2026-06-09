using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISaleOrderRequestFactory
    {
        SaleOrderRequest Create(Buyer buyer, SaleOrder saleOrder, DateTime dateTime);
    }
}
