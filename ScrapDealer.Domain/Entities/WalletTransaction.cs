using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class WalletTransaction : AggregateRoot<Guid>
    {
        public Amount Amount { get; set; }
        public Description Description { get; set; }

        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }

        public Wallet Wallet { get; set; }
        public Guid WalletId { get; set; }
    }
}
