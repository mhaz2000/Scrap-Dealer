namespace ScrapDealer.Application.DTO;

public record MessageDto
{
    public Guid Id { get; set; }
    public ICollection<Guid> Attachments { get; set; } = [];
    public Guid SenderId { get; set; }
    public required string Sender { get; set; }
    public required string Content { get; set; }
    public bool IsUserMessage { get; set; }
}
