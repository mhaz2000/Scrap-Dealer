using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IContractFactory
    {
        Contract Create(Guid saleOrderId, Amount amount, Amount commissionAmount, Guid buyerId);
    }
}
