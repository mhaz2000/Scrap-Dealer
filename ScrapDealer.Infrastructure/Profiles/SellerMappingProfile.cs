using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class SellerMappingProfile : Profile
    {
        public SellerMappingProfile()
        {
            CreateMap<SellerReadModel, SellerProfileDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(s => s.User.Phone))
                .ForMember(dest => dest.Verified, opt => opt.MapFrom(b => b.Verified));
        }
    }
}
