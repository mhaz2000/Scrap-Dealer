using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class WalletTransactionReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public WalletReadModel Wallet { get; set; }
        public Guid WalletId { get; set; }

    }
}
