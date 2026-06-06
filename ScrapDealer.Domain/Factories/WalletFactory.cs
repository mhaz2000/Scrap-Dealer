using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Wallets;

namespace ScrapDealer.Domain.Factories
{
    public class WalletFactory : IWalletFactory
    {
        public Wallet Create(Seller? seller, Buyer? buyer)
        {
            var walletNumberValue = WalletNumber.GenerateFromNationalCode(buyer?.NationalCode ?? seller?.NationalCode);
            return new Wallet(walletNumberValue, Amount.Create(0), seller, buyer);
        }
    }
}
