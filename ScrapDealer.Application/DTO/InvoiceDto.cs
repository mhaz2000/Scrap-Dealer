using DNTPersianUtils.Core;
using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO;

public record InvoiceDto
{
    public string SellerName { get; set; }
    public string BuyerName { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string DateTimeText => DateTime.ToShortPersianDateTimeString();
    public IEnumerable<InvoiceItemDto> Items { get; set; }
    public Guid Id { get; set; }
    public int InvoiceCode { get; set; }
    public int SaleOrderCode { get; set; }
    public InvoiceStatus Status { get; set; }
    public double? SellerLatitude { get; set; }
    public double? SellerLongitude { get; set; }
    public double? BuyerLatitude { get; set; }
    public double? BuyerLongitude { get; set; }
}

public record InvoiceItemDto
{
    public string Subcategory { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public double? Weight { get; set; }
    public SaleType SaleType { get; set; }
}
