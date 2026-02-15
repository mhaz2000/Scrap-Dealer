namespace ScrapDealer.Application.DTO;

public record RewardDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public string UserFullName { get; set; }
    public string? Description { get; set; }
}
