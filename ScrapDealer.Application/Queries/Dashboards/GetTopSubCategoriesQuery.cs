using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Dashboards;

public class GetTopSubCategoriesQuery : IQuery<IEnumerable<TopSubCategoryDto>>
{
    public int TopN { get; set; } = 10;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
