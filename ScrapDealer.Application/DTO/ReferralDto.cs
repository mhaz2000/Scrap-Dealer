namespace ScrapDealer.Application.DTO;

public record ReferralDto
{
    public Guid Id { get; set; }
    public Guid ReferrerUserId { get; set; }
    public string ReferrerFullName { get; set; }
    public string ReferrerPhone { get; set; }
    public Guid RefereeUserId { get; set; }
    public string RefereeFullName { get; set; }
    public string RefereePhone { get; set; }
    public string Code { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
