namespace ScrapDealer.Application.DTO;

public record AdminDashboardStatisticsDto
{
    public int InstalledNumber { get; set; }
    public int ActiveUsers { get; set; }
    public int Sellers { get; set; }
    public int Buyers { get; set; }
    public int Invoices { get; set; }
    public decimal TotalInvoicesAmount { get; set; }
}