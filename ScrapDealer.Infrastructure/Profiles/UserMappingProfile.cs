using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserReadModel, UserDto>()
                .ForMember(dest=> dest.Role, src=> src.MapFrom(t=> t.UserRoles.FirstOrDefault() != null ? t.UserRoles.FirstOrDefault()!.Role.Name : ""))
                .ForMember(dest=> dest.ReferralCode, src=> src.MapFrom(t=> t.ReferralCode));
        }
    }
}
