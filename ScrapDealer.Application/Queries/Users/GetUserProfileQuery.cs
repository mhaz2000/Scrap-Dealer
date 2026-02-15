using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Users
{
    public record GetUserProfileQuery(Guid userId) : IQuery<UserDto>;
}
