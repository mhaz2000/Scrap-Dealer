using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class ContractReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public SaleOrderReadModel SaleOrder { get; private set; }
        public Guid SaleOrderId { get; private set; }
        public ContractStatus Status { get; private set; }
        public decimal Amount { get; private set; }
        public decimal CommissionAmount { get; private set; }
        public BuyerReadModel Buyer { get; private set; }
        public Guid BuyerId { get; private set; }

    }
}
