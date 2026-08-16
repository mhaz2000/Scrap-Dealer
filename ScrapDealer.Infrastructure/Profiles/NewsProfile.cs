using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class NewsProfile : Profile
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NewsProfile()
        {
            CreateMap<NewsReadModel, NewsDto>()
                .ForCtorParam(
                    "content",
                    opt => opt.MapFrom(src => DeserializeContent(src.Content))
                );
        }

        private static ICollection<NewsContentBlockDto> DeserializeContent(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<NewsContentBlockDto>();

            return JsonSerializer.Deserialize<List<NewsContentBlockDto>>(
                       json,
                       JsonOptions)
                   ?? new List<NewsContentBlockDto>();
        }
    }
}