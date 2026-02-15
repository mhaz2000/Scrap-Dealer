namespace ScrapDealer.Application.DTO;

public record TicketDto
{
    public Guid Id { get; set; }
    public ulong Number { get; set; }
    public required string Title { get; set; }
    public required string CreatedBy { get; set; }
    public bool Opened { get; set; }
}
