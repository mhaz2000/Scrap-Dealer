using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Categories
{
    public record GetCategorySubCategoriesQuery(Guid CategoryId) : PaginationQuery, IQuery<PaginatedResult<SubCategoryDto>>;
}
