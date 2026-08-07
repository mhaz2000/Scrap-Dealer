using DNTPersianUtils.Core;

namespace ScrapDealer.Application.DTO;

public record InvoiceListDto
{
    public string SellerName { get; set; }
    public string BuyerName { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string DateTimeText => DateTime.ToShortPersianDateTimeString();

    public Guid Id { get; set; }
    public int InvoiceCode { get; set; }
    public int SaleOrderCode { get; set; }
}
