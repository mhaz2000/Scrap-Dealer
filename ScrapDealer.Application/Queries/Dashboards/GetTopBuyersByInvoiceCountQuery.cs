using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Dashboards;

public class GetTopBuyersByInvoiceCountQuery : IQuery<IEnumerable<TopBuyerDto>>
{
    public int TopN { get; set; } = 10;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
