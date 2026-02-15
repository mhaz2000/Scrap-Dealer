using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Categories
{
    public record GetCategoryQuery(Guid Id) : IQuery<CategoryDto>;
}
