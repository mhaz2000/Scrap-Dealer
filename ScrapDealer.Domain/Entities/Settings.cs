using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Settings;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Settings : AggregateRoot<Guid>
    {
        public CommissionRate? BuyerCommissionRate { get; private set; }
        public Amount? BuyerCommissionFixedAmount { get; private set; }

        public Settings()
        {
            
        }

        public Settings(CommissionRate? buyerCommissionRate, Amount? buyerCommissionFixedAmount)
        {
            BuyerCommissionFixedAmount = buyerCommissionFixedAmount;
            BuyerCommissionRate = buyerCommissionRate;
        }

        internal void Update(CommissionRate? buyerCommissionRate, Amount? buyerCommissionFixedAmount)
        {
            BuyerCommissionFixedAmount = buyerCommissionFixedAmount;
            BuyerCommissionRate = buyerCommissionRate;
        }
    }
}
