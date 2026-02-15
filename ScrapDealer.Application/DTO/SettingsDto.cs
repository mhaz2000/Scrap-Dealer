namespace ScrapDealer.Application.DTO;
public record SettingsDto
{
    public float? BuyerCommissionRate { get; private set; }
    public decimal? BuyerCommissionFixedAmount { get; private set; }
}
