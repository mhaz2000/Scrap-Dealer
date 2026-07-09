using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Dashboards;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Dashboards;

internal class GetTopBuyersByTotalAmountHandler : IQueryHandler<GetTopBuyersByTotalAmountQuery, IEnumerable<TopBuyerDto>>
{
    private readonly DbSet<InvoiceReadModel> _invoices;

    public GetTopBuyersByTotalAmountHandler(ReadDbContext context)
    {
        _invoices = context.Invoices;
    }

    public async Task<IEnumerable<TopBuyerDto>> Handle(GetTopBuyersByTotalAmountQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices.IgnoreQueryFilters()
            .Include(i => i.Contract)
                .ThenInclude(c => c.Buyer)
                    .ThenInclude(b => b.User)
            .AsQueryable();

        // Apply date filter if provided
        if (query.FromDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime >= query.FromDate.Value);

        if (query.ToDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime <= query.ToDate.Value);

        var result = await dbQuery
            .Where(i => i.Contract.Buyer != null)
            .GroupBy(i => new
            {
                BuyerId = i.Contract.Buyer.Id,
                BuyerCode = i.Contract.Buyer.Code,
                FirstName = i.Contract.Buyer.FirstName,
                LastName = i.Contract.Buyer.LastName,
                Phone = i.Contract.Buyer.User.Phone
            })
            .Select(group => new TopBuyerDto
            {
                BuyerId = group.Key.BuyerId,
                Code = group.Key.BuyerCode.ToString(),
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