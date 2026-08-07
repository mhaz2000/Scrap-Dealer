using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{

    public class Invoice : AggregateRoot<Guid>
    {
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        public Amount Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Code Code { get; private set; }


        private readonly List<InvoiceItem> _items = new List<InvoiceItem>();
        public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

        public void AddItem(InvoiceItem item)
            => _items.Add(item);

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
        }
    }
}
