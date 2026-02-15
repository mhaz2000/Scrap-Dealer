using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories
{

    public class ContractFactory : IContractFactory
    {
        public Contract Create(Guid saleOrderId, Amount amount, Amount commissionAmount, Guid buyerId)
        {
            var amountValue = Amount.Create(amount);
            var commissionAmountValue = Amount.Create(commissionAmount);

            return new Contract(saleOrderId, amountValue, commissionAmountValue, buyerId);
        }
    }
}
