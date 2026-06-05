using AutoMapper;
using DNTPersianUtils.Core;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<SubCategoryReadModel, SubCategoryDto>()
                .ConstructUsing(c => new SubCategoryDto(c.Id, c.Name, c.MinPrice, c.MaxPrice, c.LastUpdate.ToPersianDateTimeString("yyyy/MM/dd HH:mm", false), c.CategoryId, c.Images))
                .ForMember(dest => dest.LastUpdate, src => src.MapFrom(c => c.LastUpdate.ToPersianDateTimeString("yyyy/MM/dd HH:mm", false)));

            CreateMap<CategoryReadModel, CategoryDto>()
                .ConstructUsing(c => new CategoryDto(c.Id, c.Name, c.MinPrice, c.MaxPrice, c.SubCategories.Where(s => !s.IsDeleted)
                .Select(s => new SubCategoryDto(s.Id, s.Name, s.MinPrice, s.MaxPrice, s.LastUpdate.ToPersianDateTimeString("yyyy/MM/dd HH:mm", false), s.CategoryId, s.Images)).ToList(), c.Images))
                .ForMember(dest=> dest.LastUpdate, src=> src.MapFrom(c => c.LastUpdate.ToPersianDateTimeString("yyyy/MM/dd HH:mm", false)));
        }
    }
}
