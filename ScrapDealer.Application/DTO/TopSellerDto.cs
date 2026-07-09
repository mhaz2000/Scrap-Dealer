namespace ScrapDealer.Application.DTO;

public class TopSellerDto : TopItemDto
{
    public Guid SellerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
