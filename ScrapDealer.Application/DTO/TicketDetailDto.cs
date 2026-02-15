namespace ScrapDealer.Application.DTO;

public record TicketDetailDto
{
    public Guid Id { get; set; }
    public ulong TicketNumber { get; set; }
    public required string Title { get; set; }
    public bool Opened { get; set; }
    public IEnumerable<MessageDto> Messages { get; set; }
}
