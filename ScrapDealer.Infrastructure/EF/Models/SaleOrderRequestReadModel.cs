namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SaleOrderRequestReadModel
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SaleOrderId { get; set; }
        public BuyerReadModel Buyer { get; set; }
        public SaleOrderReadModel SaleOrder { get; set; }
        public DateTime DateTime { get; set; }
    }
}
