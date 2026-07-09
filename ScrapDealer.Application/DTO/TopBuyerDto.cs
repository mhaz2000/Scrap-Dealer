namespace ScrapDealer.Application.DTO;

public class TopBuyerDto : TopItemDto
{
    public Guid BuyerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
