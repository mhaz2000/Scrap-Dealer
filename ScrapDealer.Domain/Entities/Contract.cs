using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Contract : AggregateRoot<Guid>
    {
        public SaleOrder SaleOrder { get; private set; }
        public Guid SaleOrderId { get; private set; }
        public ContractStatus Status { get; private set; }
        public Amount Amount { get; private set; }
        public Amount CommissionAmount { get; private set; }
        public Buyer Buyer { get; private set; }
        public Guid BuyerId { get; private set; }

        public Contract(Guid saleOrderId, Amount amount, Amount commissionAmount, Guid buyerId)
        {
            SaleOrderId = saleOrderId;
            Status = ContractStatus.AcceptByBuyer;
            Amount = amount;
            CommissionAmount = commissionAmount;
            BuyerId = buyerId;
        }

        public void SetStatus(ContractStatus status) => Status = status;

        public void SetAmount(decimal amount)
            => Amount = Amount.Create(amount);
    }
}
