namespace ScrapDealer.Application.DTO;

public class DashboardSummaryDto
{
    public IEnumerable<TopSubCategoryDto> TopSubCategories { get; set; } = Enumerable.Empty<TopSubCategoryDto>();
    public IEnumerable<TopBuyerDto> TopBuyersByInvoiceCount { get; set; } = Enumerable.Empty<TopBuyerDto>();
    public IEnumerable<TopSellerDto> TopSellersByInvoiceCount { get; set; } = Enumerable.Empty<TopSellerDto>();
    public IEnumerable<TopBuyerDto> TopBuyersByTotalAmount { get; set; } = Enumerable.Empty<TopBuyerDto>();
    public IEnumerable<TopSellerDto> TopSellersByTotalAmount { get; set; } = Enumerable.Empty<TopSellerDto>();
}