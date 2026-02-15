using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Reward : AggregateRoot<Guid>
    {
        public Amount Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string? Description { get; set; }

        public Reward()
        {
            
        }
        public Reward(Amount amount, string? description, User user)
        {
            Amount = amount;
            Description = description;
            Date = DateTime.Now;
            UserId = user.Id;
            User = user;
        }
    }
}
