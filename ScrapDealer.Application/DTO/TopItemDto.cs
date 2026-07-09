namespace ScrapDealer.Application.DTO;

public class TopItemDto
{
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public int InvoiceCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AverageAmount { get; set; }
}
