using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles;

public class InitialBuyerMappingProfile : Profile
{
    public InitialBuyerMappingProfile() => CreateMap<InitialBuyerReadModel, InitialBuyerDto>();
}
