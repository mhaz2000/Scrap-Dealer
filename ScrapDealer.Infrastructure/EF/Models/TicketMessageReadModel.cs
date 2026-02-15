namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class TicketMessageReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public required string Content { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Guid> Attachments { get; set; }
        public Guid SenderId { get; set; }
        public required UserReadModel Sender { get; set; }
        public TicketReadModel Ticket { get; set; }
        public Guid TicketId { get; set; }

    }
}
