using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories
{
    public class RewardFactory : IRewardFactory
    {
        public Reward Create(Amount amount, string? description, User user)
        {
            var amountValue = Amount.Create(amount);

            return new Reward(amountValue, description, user);
        }
    }
}
