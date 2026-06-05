using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Wallets;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Wallet : AggregateRoot<Guid>
    {
        public WalletNumber Number { get; set; }
        public Amount Balance { get; set; }
        public Seller? Seller { get; set; }
        public Buyer? Buyer { get; set; }

        public Guid? SellerId { get; set; }
        public Guid? BuyerId { get; set; }

        private Wallet() { }
        public Wallet(WalletNumber number, Amount balance, Seller? seller, Buyer? buyer)
        {
            Number = number;
            Balance = balance;
            Seller = seller;
            Buyer = buyer;
            SellerId = seller.Id;
            BuyerId = buyer.Id;
        }
    }
}
