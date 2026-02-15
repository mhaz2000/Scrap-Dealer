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
    internal class GetSubCategoryHandler : IQueryHandler<GetSubCategoryQuery, SubCategoryDto>
    {
        private readonly DbSet<SubCategoryReadModel> _subCategories;
        private readonly IMapper _mapper;

        public GetSubCategoryHandler(ReadDbContext context, IMapper mapper)
        {
            _subCategories = context.SubCategories;
            _mapper = mapper;
        }

        public async Task<SubCategoryDto> Handle(GetSubCategoryQuery query, CancellationToken cancellationToken)
        {
            var category = await _subCategories.FirstOrDefaultAsync(c => c.Id == query.Id);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            return _mapper.Map<SubCategoryDto>(category);
        }
    }
}
