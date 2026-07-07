using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Users
{
    public record GetUsersQuery(bool IsActive = false) : PaginationQuery, IQuery<PaginatedResult<UserDto>>;
}
