using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<SubCategoryReadModel, SubCategoryDto>()
                .ConstructUsing(c => new SubCategoryDto(c.Id, c.Name, c.MinPrice, c.MaxPrice, c.CategoryId, c.Images));

            CreateMap<CategoryReadModel, CategoryDto>()
                .ConstructUsing(c => new CategoryDto(c.Id, c.Name, c.MinPrice, c.MaxPrice, c.SubCategories.Where(s => !s.IsDeleted)
                .Select(s => new SubCategoryDto(s.Id, s.Name, s.MinPrice, s.MaxPrice, s.CategoryId, s.Images)).ToList(), c.Images));
        }
    }
}
