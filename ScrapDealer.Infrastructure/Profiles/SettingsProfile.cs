using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles;
public class SettingsProfile : Profile
{
    public SettingsProfile()
    {
        CreateMap<SettingsReadModel, SettingsDto>();
    }
}
