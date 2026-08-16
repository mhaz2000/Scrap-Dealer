using ScrapDealer.Application.DTO;

namespace ScrapDealer.Application.DTO
{
    public record NewsDto(Guid Id, string Title, string Summary, ICollection<NewsContentBlockDto> Content, DateTime CreatedAt, DateTime UpdatedAt);
}