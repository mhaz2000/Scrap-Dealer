using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Categories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Categories
{
    internal class GetCategorySubCategoriesHandler : IQueryHandler<GetCategorySubCategoriesQuery, PaginatedResult<SubCategoryDto>>
    {
        private readonly DbSet<CategoryReadModel> _categories;
        private readonly IMapper _mapper;

        public GetCategorySubCategoriesHandler(ReadDbContext context, IMapper mapper)
        {
            _categories = context.Categories;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SubCategoryDto>> Handle(GetCategorySubCategoriesQuery query, CancellationToken cancellationToken)
        {
            var category = await _categories.Include(c => c.SubCategories).FirstOrDefaultAsync(c => c.Id == query.CategoryId);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            var dbQuery = category.SubCategories.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%"));

            var subCategories = dbQuery.AsNoTracking();
            var paginatedResult = subCategories.
                ToPaginatedResult<SubCategoryReadModel, SubCategoryDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
