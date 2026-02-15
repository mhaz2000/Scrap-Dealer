using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Tickets;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ITicketFactory
    {
        Ticket Create(TicketTitle title, ulong? number);
        TicketMessage CreateMessage(User Sender, MessageContent content, ICollection<Guid> attachments);
    }
}
