using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Categories
{
    public record GetCategoriesQuery : PaginationQuery, IQuery<PaginatedResult<CategoryDto>>;
}
