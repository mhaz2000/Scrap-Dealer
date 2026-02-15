namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class TicketReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public required string Title { get; set; }
        public bool Opened { get; set; }
        public ICollection<TicketMessageReadModel> Messages { get; set; }
        public ulong Number { get; set; }
    }
}
