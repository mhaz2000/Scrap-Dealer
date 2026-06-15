using System.Collections.ObjectModel;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class InvoiceReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Collection<InvoiceItemReadModel> Items { get; set; } = [];
        public ContractReadModel Contract { get; set; }
        public Guid ContractId { get; set; }
    }
}
