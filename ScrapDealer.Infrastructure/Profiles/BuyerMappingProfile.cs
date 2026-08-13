using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class BuyerMappingProfile : Profile
    {
        public BuyerMappingProfile()
        {
            CreateMap<BuyerReadModel, NearbyBuyerDto>();
            CreateMap<BuyerReadModel, BuyerProfileDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(b => b.User.Phone))
                .ForMember(dest => dest.HasCar, opt => opt.MapFrom(b => b.CarCardFileId != null))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(b => b.User.IsActive))
                .ForMember(dest => dest.Verified, opt => opt.MapFrom(b => b.Verified))
                .ForMember(dest => dest.ReferralCode, opt => opt.MapFrom(b => b.User.ReferralCode));
        }
    }
}
