using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Tickets;

namespace ScrapDealer.Domain.Factories
{
    public class TicketFactory : ITicketFactory
    {
        public TicketMessage CreateMessage(User sender, MessageContent content, ICollection<Guid> attachments)
        {
            var contenetValue = MessageContent.Create(content);

            return new TicketMessage(sender, contenetValue, attachments);
        }

        public Ticket Create(TicketTitle title, ulong? number)
        {
            var titleValue = TicketTitle.Create(title);

            return new Ticket(titleValue, (number ?? 10000) + 1);
        }
    }
}
