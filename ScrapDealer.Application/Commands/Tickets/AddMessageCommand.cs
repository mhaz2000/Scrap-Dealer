using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Tickets
{
    public record AddMessageCommand : ICommand
    {
        public Guid TicketId { get; set; }
        public required string Content { get; set; }
        public ICollection<Guid> Attachments { get; set; } = [];
        public Guid? UserId { get; set; }
    }
}
