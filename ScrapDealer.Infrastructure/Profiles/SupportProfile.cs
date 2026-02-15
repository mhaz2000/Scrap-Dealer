using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class SupportProfile : Profile
    {
        public SupportProfile()
        {
            CreateMap<UserReadModel, SupportDto>()
                .ForMember(t => t.PhoneNumber, src => src.MapFrom(t => t.Phone));
        }
    }
}
