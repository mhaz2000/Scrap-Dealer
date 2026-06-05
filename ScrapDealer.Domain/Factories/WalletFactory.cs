using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Wallets;

namespace ScrapDealer.Domain.Factories
{
    public class WalletFactory : IWalletFactory
    {
        public Wallet Create(Seller? seller, Buyer? buyer, string walletNumber)
        {
            var walletNumberText = WalletNumber.GenerateFromNationalCode(buyer?.NationalCode ?? seller?.NationalCode);
            var walletNumberValue = WalletNumber.Create(walletNumberText);
            return new Wallet(walletNumberValue, Amount.Create(0), seller, buyer);
        }
    }
}
