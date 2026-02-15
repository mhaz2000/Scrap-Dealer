using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Users
{
    public record GetUsersQuery : PaginationQuery, IQuery<PaginatedResult<UserDto>>;
}
