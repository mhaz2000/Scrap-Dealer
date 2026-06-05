using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IWalletFactory
    {
        Wallet Create(Seller? seller, Buyer? buyer, string walletNumber);
    }
}
