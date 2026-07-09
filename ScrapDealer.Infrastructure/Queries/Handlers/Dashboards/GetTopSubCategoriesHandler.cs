using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Dashboards;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Dashboards;

internal class GetTopSubCategoriesHandler : IQueryHandler<GetTopSubCategoriesQuery, IEnumerable<TopSubCategoryDto>>
{
    private readonly DbSet<InvoiceReadModel> _invoices;
    private readonly IMapper _mapper;

    public GetTopSubCategoriesHandler(ReadDbContext context, IMapper mapper)
    {
        _invoices = context.Invoices;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TopSubCategoryDto>> Handle(GetTopSubCategoriesQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices.IgnoreQueryFilters()
            .Include(i => i.Items)
                .ThenInclude(item => item.SubCategory)
                    .ThenInclude(sub => sub!.Category)
            .Include(i => i.Contract)
            .AsQueryable();

        if (query.FromDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime >= query.FromDate.Value);

        if (query.ToDate.HasValue)
            dbQuery = dbQuery.Where(i => i.DateTime <= query.ToDate.Value);

        var result = await dbQuery
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
            .ToListAsync(cancellationToken);

        return result;
    }
}