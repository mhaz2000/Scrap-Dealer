using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Categories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Categories
{
    internal class GetSubCategoriesHandler : IQueryHandler<GetSubCategoriesQuery, PaginatedResult<SubCategoryDto>>
    {
        private readonly DbSet<SubCategoryReadModel> _categories;
        private readonly DbSet<CategoryPriceHistoryReadModel> _categoryPriceHistories;
        private readonly IMapper _mapper;

        public GetSubCategoriesHandler(ReadDbContext context, IMapper mapper)
        {
            _categoryPriceHistories = context.CategoryPriceHistories;
            _categories = context.SubCategories;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SubCategoryDto>> Handle(GetSubCategoriesQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _categories.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%"));

            var categories = dbQuery.AsNoTracking();
            var paginatedResult = await categories.
                ToPaginatedResultAsync<SubCategoryReadModel, SubCategoryDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            await SetLasPrices(paginatedResult);

            return paginatedResult;
        }

        private async Task SetLasPrices(PaginatedResult<SubCategoryDto> result)
        {
            var categories = result.Data.Select(s => s.Id).ToList();

            var now = DateTime.Now;
            var weekAgo = now.AddDays(-7);
            var monthAgo = now.AddMonths(-1);
            var yearAgo = now.AddYears(-1);

            var histories = await _categoryPriceHistories
                            .Where(t => t.SubCategoryId.HasValue && categories.Contains(t.SubCategoryId.Value))
                            .GroupBy(t => t.SubCategoryId)
                            .Select(g => new
                            {
                                Id = g.Key,

                                LastRecord = g
                                    .OrderByDescending(x => x.DateTime)
                                    .Select(x => x)
                                    .Skip(1)
                                    .FirstOrDefault(),

                                LastWeekRecord = g
                                    .Where(x => x.DateTime <= weekAgo)
                                    .OrderByDescending(x => x.DateTime)
                                    .FirstOrDefault(),

                                LastMonthRecord = g
                                    .Where(x => x.DateTime <= monthAgo)
                                    .OrderByDescending(x => x.DateTime)
                                    .FirstOrDefault(),

                                LastYearRecord = g
                                    .Where(x => x.DateTime <= yearAgo)
                                    .OrderByDescending(x => x.DateTime)
                                    .FirstOrDefault()
                            })
                            .ToListAsync();

            foreach (var item in result.Data)
            {
                var categoryHistory = histories.FirstOrDefault(t => t.Id == item.Id);
                item.LastMaxPrice = categoryHistory?.LastRecord?.MaxPrice ?? item.MaxPrice;
                item.LastMinPrice = categoryHistory?.LastRecord?.MinPrice ?? item.MinPrice;

                item.LastWeekMinPrice = categoryHistory?.LastWeekRecord?.MinPrice ?? 0;
                item.LastWeekMaxPrice = categoryHistory?.LastWeekRecord?.MaxPrice ?? 0;

                item.LastMonthMinPrice = categoryHistory?.LastMonthRecord?.MinPrice ?? 0;
                item.LastMonthMaxPrice = categoryHistory?.LastMonthRecord?.MaxPrice ?? 0;

                item.LastYearMinPrice = categoryHistory?.LastYearRecord?.MinPrice ?? 0;
                item.LastYearMaxPrice = categoryHistory?.LastYearRecord?.MaxPrice ?? 0;
            }
        }

    }
}
