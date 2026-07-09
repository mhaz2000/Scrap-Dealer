using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Dashboards;

public class GetDashboardSummaryQuery : IQuery<DashboardSummaryDto>
{
    public int TopN { get; set; } = 5;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}