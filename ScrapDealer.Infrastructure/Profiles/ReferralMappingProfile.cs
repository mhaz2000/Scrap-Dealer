using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class ReferralMappingProfile : Profile
    {
        public ReferralMappingProfile()
        {
            CreateMap<ReferralReadModel, ReferralDto>()
                .ForMember(dest => dest.ReferrerPhone, src => src.MapFrom(t => t.ReferrerUser != null ? t.ReferrerUser.Phone : string.Empty))
                .ForMember(dest => dest.ReferrerFullName, src => src.MapFrom(t => t.ReferrerUser != null ? t.ReferrerUser.FullName : string.Empty))
                .ForMember(dest => dest.RefereeFullName, src => src.MapFrom(t => t.RefereeUser != null ? t.RefereeUser.FullName : string.Empty))
                .ForMember(dest => dest.RefereePhone, src => src.MapFrom(t => t.RefereeUser != null ? t.RefereeUser.Phone : string.Empty))
                .ForMember(dest => dest.Status, src => src.MapFrom(t => t.Status.ToString()));
        }
    }
}
