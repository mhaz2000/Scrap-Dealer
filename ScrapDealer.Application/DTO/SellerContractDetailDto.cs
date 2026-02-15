namespace ScrapDealer.Application.DTO;

public record SellerContractDetailDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public required string BuyerName { get; set; }
    public float BuyerScore { get; set; }
    public bool IsFixedLocation { get; set; }
    public string PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? NumberPlate { get; set; }
}
