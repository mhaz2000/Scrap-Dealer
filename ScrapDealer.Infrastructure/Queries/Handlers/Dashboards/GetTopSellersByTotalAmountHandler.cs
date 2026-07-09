using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Dashboards;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Dashboards;

internal class GetTopSellersByTotalAmountHandler : IQueryHandler<GetTopSellersByTotalAmountQuery, IEnumerable<TopSellerDto>>
{
    private readonly DbSet<InvoiceReadModel> _invoices;

    public GetTopSellersByTotalAmountHandler(ReadDbContext context)
    {
        _invoices = context.Invoices;
    }

    public async Task<IEnumerable<TopSellerDto>> Handle(GetTopSellersByTotalAmountQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices.IgnoreQueryFilters()
            .Include(i => i.Contract)
                .ThenInclude(c => c.SaleOrder)
                    .ThenInclude(so => so.Seller)
                        .ThenInclude(s => s.User)
            .AsQueryable();

        // Apply date filter if provided
        if (query.FromDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime >= query.FromDate.Value);

        if (query.ToDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime <= query.ToDate.Value);

        var result = await dbQuery
            .Where(i => i.Contract.SaleOrder.Seller != null)
            .GroupBy(i => new
            {
                SellerId = i.Contract.SaleOrder.Seller.Id,
                SellerCode = i.Contract.SaleOrder.Seller.Code,
                FirstName = i.Contract.SaleOrder.Seller.FirstName,
                LastName = i.Contract.SaleOrder.Seller.LastName,
                Phone = i.Contract.SaleOrder.Seller.User.Phone
            })
            .Select(group => new TopSellerDto
            {
                SellerId = group.Key.SellerId,
                Code = group.Key.SellerCode.ToString(),
                FullName = $"{group.Key.FirstName} {group.Key.LastName}",
                Phone = group.Key.Phone,
                InvoiceCount = group.Count(),
                TotalAmount = group.Sum(i => i.Amount),
                AverageAmount = group.Average(i => i.Amount),
                Name = $"{group.Key.FirstName} {group.Key.LastName}"
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(query.TopN)
            .ToListAsync(cancellationToken);

        return result;
    }
}
