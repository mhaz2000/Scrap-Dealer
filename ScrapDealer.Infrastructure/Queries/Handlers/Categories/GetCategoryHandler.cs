using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Categories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Categories
{
    internal class GetCategoryHandler : IQueryHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly DbSet<CategoryReadModel> _categories;
        private readonly IMapper _mapper;

        public GetCategoryHandler(ReadDbContext context, IMapper mapper)
        {
            _categories = context.Categories;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
        {
            var category = await _categories.Include(c => c.SubCategories).FirstOrDefaultAsync(c => c.Id == query.Id);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
