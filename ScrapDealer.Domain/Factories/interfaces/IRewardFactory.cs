using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IRewardFactory
    {
        Reward Create(Amount amount, string? description, User user);
    }
}
