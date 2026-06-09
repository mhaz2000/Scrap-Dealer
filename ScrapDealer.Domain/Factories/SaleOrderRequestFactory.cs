using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;

namespace ScrapDealer.Domain.Factories
{
    public class SaleOrderRequestFactory : ISaleOrderRequestFactory
    {
        public SaleOrderRequest Create(Buyer buyer, SaleOrder saleOrder, DateTime dateTime)
        {
            return new SaleOrderRequest(saleOrder, buyer, dateTime);
        }
    }
}
