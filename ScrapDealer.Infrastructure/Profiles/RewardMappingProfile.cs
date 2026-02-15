using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class RewardMappingProfile : Profile
    {
        public RewardMappingProfile()
        {
            CreateMap<RewardReadModel, RewardDto>()
                .ForMember(dest=> dest.UserFullName, src=> src.MapFrom(t=> t.User.FullName));
        }
    }
}
