using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Permissions;
public record GetPermissionsQuery : PaginationQuery, IQuery<PaginatedResult<string>>;
