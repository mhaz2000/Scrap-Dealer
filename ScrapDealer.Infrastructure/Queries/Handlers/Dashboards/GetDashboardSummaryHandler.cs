using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Dashboards;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Dashboards;

internal class GetDashboardSummaryHandler : IQueryHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
{
    private readonly DbSet<InvoiceReadModel> _invoices;

    public GetDashboardSummaryHandler(ReadDbContext context)
    {
        _invoices = context.Invoices;
    }

    public async Task<DashboardSummaryDto> Handle(GetDashboardSummaryQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices.IgnoreQueryFilters()
            .Include(i => i.Items)
                .ThenInclude(item => item.SubCategory)
                    .ThenInclude(sub => sub!.Category)
            .Include(i => i.Contract)
                .ThenInclude(c => c.Buyer)
                    .ThenInclude(b => b.User)
            .Include(i => i.Contract)
                .ThenInclude(c => c.SaleOrder)
                    .ThenInclude(so => so.Seller)
                        .ThenInclude(s => s.User)
            .AsQueryable();

        if (query.FromDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime >= query.FromDate.Value);

        if (query.ToDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime <= query.ToDate.Value);

        var invoicesList = await dbQuery.ToListAsync(cancellationToken);

        var summary = new DashboardSummaryDto();

        summary.TopSubCategories = invoicesList
            .SelectMany(i => i.Items)
            .Where(item => item.SubCategory != null)
            .GroupBy(item => new
            {
                SubCategoryId = item.SubCategory!.Id,
                SubCategoryName = item.SubCategory.Name,
                CategoryName = item.SubCategory.Category.Name
            })
            .Select(group => new TopSubCategoryDto
            {
                SubCategoryId = group.Key.SubCategoryId,
                Name = group.Key.SubCategoryName,
                CategoryName = group.Key.CategoryName,
                InvoiceCount = group.Count(),
                TotalAmount = group.Sum(item => item.Amount),
                AverageAmount = group.Average(item => item.Amount)
            })
            .OrderByDescending(x => x.InvoiceCount)
            .Take(query.TopN)
            .ToList();

        summary.TopBuyersByInvoiceCount = invoicesList
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
            .OrderByDescending(x => x.InvoiceCount)
            .Take(query.TopN)
            .ToList();

        summary.TopSellersByInvoiceCount = invoicesList
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
            .OrderByDescending(x => x.InvoiceCount)
            .Take(query.TopN)
            .ToList();

        summary.TopBuyersByTotalAmount = invoicesList
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
            .ToList();

        summary.TopSellersByTotalAmount = invoicesList
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
            .ToList();

        return summary;
    }
}