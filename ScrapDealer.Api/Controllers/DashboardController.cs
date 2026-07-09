using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Dashboards;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Api.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
public class DashboardController(IQueryDispatcher _queryDispatcher) : BaseController
{
    [HttpGet("TopSubCategories")]
    public async Task<ActionResult<IEnumerable<TopSubCategoryDto>>> GetTopSubCategories(
        [FromQuery] int topN = 10,
        [FromQuery] string? fromDate = null,
        [FromQuery] string? toDate = null)
    {
        var enFromDate = fromDate?.ToGregorianDateTime();
        var enToDate = toDate?.ToGregorianDateTime();
        var query = new GetTopSubCategoriesQuery
        {
            TopN = topN,
            FromDate = enFromDate,
            ToDate = enToDate
        };

        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }


    [HttpGet("TopBuyers/ByInvoiceCount")]
    public async Task<ActionResult<IEnumerable<TopBuyerDto>>> GetTopBuyersByInvoiceCount(
            [FromQuery] int topN = 10,
            [FromQuery] string? fromDate = null,
            [FromQuery] string? toDate = null)
    {
        var enFromDate = fromDate?.ToGregorianDateTime();
        var enToDate = toDate?.ToGregorianDateTime();
        var query = new GetTopBuyersByInvoiceCountQuery
        {
            TopN = topN,
            FromDate = enFromDate,
            ToDate = enToDate
        };

        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpGet("TopSellers/ByInvoiceCount")]
    public async Task<ActionResult<IEnumerable<TopSellerDto>>> GetTopSellersByInvoiceCount(
       [FromQuery] int topN = 10,
            [FromQuery] string? fromDate = null,
            [FromQuery] string? toDate = null)
    {
        var enFromDate = fromDate?.ToGregorianDateTime();
        var enToDate = toDate?.ToGregorianDateTime();
        var query = new GetTopSellersByInvoiceCountQuery
        {
            TopN = topN,
            FromDate = enFromDate,
            ToDate = enToDate
        };

        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpGet("TopBuyers/ByTotalAmount")]
    public async Task<ActionResult<IEnumerable<TopBuyerDto>>> GetTopBuyersByTotalAmount(
        [FromQuery] int topN = 10,
        [FromQuery] string? fromDate = null,
        [FromQuery] string? toDate = null)
    {
        var enFromDate = fromDate?.ToGregorianDateTime();
        var enToDate = toDate?.ToGregorianDateTime();
        var query = new GetTopBuyersByTotalAmountQuery
        {
            TopN = topN,
            FromDate = enFromDate,
            ToDate = enToDate
        };

        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpGet("TopSellers/ByTotalAmount")]
    public async Task<ActionResult<IEnumerable<TopSellerDto>>> GetTopSellersByTotalAmount(
        [FromQuery] int topN = 10,
        [FromQuery] string? fromDate = null,
        [FromQuery] string? toDate = null)
    {
        var enFromDate = fromDate?.ToGregorianDateTime();
        var enToDate = toDate?.ToGregorianDateTime();
        var query = new GetTopSellersByTotalAmountQuery
        {
            TopN = topN,
            FromDate = enFromDate,
            ToDate = enToDate
        };

        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }

    // Combined dashboard summary endpoint
    [HttpGet("Summary")]
    public async Task<ActionResult<DashboardSummaryDto>> GetDashboardSummary(
        [FromQuery] int topN = 5,
        [FromQuery] string? fromDate = null,
        [FromQuery] string? toDate = null)
    {
        var enFromDate = fromDate?.ToGregorianDateTime();
        var enToDate = toDate?.ToGregorianDateTime();

        var query = new GetDashboardSummaryQuery
        {
            TopN = topN,
            FromDate = enFromDate,
            ToDate = enToDate
        };

        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }
}
