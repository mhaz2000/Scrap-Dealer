using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class SaleOrderRequest : Entity<Guid>
    {
        public Guid SaleOrderId { get; set; }
        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public DateTime DateTime { get; set; }
        private SaleOrderRequest() { }
        public SaleOrderRequest(SaleOrder saleOrder, Buyer buyer, DateTime dateTime)
        {
            DateTime = dateTime;
            SaleOrderId = saleOrder.Id;
            BuyerId = buyer.Id;
            Buyer = buyer;
            SaleOrder = saleOrder;
        }
    }
}
