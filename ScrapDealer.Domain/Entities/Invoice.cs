using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.Entities
{
    public class Invoice : AggregateRoot<Guid>
    {
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        public Amount Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Code Code { get; private set; }
        public InvoiceStatus Status { get; private set; }

        private readonly List<InvoiceItem> _items = new List<InvoiceItem>();
        public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

        public void AddItem(InvoiceItem item)
            => _items.Add(item);

        public void Approve()
        {
            if (Status != InvoiceStatus.PendingSellerApproval)
                throw new BusinessException("فاکتور در وضعیت انتظار بررسی نیست.");

            Status = InvoiceStatus.Approved;
        }

        public void Reject()
        {
            if (Status != InvoiceStatus.PendingSellerApproval)
                throw new BusinessException("فاکتور قابل رد نیست.");

            Status = InvoiceStatus.Rejected;
        }

        public void Resubmit()
        {
            if (Status != InvoiceStatus.Rejected)
                throw new BusinessException("فاکتور در وضعیت رد شده نیست.");

            Status = InvoiceStatus.PendingSellerApproval;
        }

        public void ClearItems() => _items.Clear();

        public void SetAmount(decimal amount) => Amount = Amount.Create(amount);

        private Invoice()
        {

        }

        public Invoice(Contract contract, Amount amount, Code code)
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;
            Contract = contract;
            ContractId = contract.Id;
            Amount = amount;
            Code = code;
            Status = InvoiceStatus.PendingSellerApproval;
        }
    }
}
