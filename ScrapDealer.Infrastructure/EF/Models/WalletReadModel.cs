namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class WalletReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Number { get; set; } = string.Empty;
        public BuyerReadModel? Buyer { get; set; }
        public SellerReadModel? Seller { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? BuyerId { get; set; }
        public decimal Balance { get; set; }

    }
}
