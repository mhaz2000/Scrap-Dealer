using ScrapDealer.Domain.ValueObjects.Tickets;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class TicketMessage : AggregateRoot<Guid>
    {
        public Guid SenderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public User Sender { get; set; }
        public MessageContent Content { get; set; }
        public Ticket Ticket { get; set; }
        public Guid TicketId { get; set; }
        public ICollection<Guid> Attachments { get; private set; } = [];

        public TicketMessage()
        {
            
        }
        public TicketMessage(User sender, MessageContent content, ICollection<Guid> attachments)
        {
            Content = content;
            Sender = sender;
            SenderId = sender.Id;
            Attachments = attachments;
        }
    }
}
