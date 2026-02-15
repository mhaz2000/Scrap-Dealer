using ScrapDealer.Domain.ValueObjects.Tickets;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Ticket : AggregateRoot<Guid>
    {
        public TicketTitle Title { get; set; }
        public bool Opened { get; set; } = true;
        public ulong Number { get; set; }

        private readonly List<TicketMessage> _messages = new List<TicketMessage>();
        public IReadOnlyCollection<TicketMessage> Messages => _messages.AsReadOnly();

        public void AddMessage(TicketMessage message)
            => _messages.Add(message);

        public Ticket()
        {

        }

        public Ticket(TicketTitle title, ulong number)
        {
            Title = title;
            Number = number;
        }
    }
}
