using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{

    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<NewsReadModel, NewsDto>();
        }
    }
}
