using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Categories
{
    public record GetSubCategoryQuery(Guid Id) : IQuery<SubCategoryDto>;
}
