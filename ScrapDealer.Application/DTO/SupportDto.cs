namespace ScrapDealer.Application.DTO;
public record SupportDto
{
    public Guid Id { get; set; }
    public Guid UserId => Id;
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
}
