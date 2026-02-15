using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Permissions;

public record GetSupportPermissionsQuery : PaginationQuery, IQuery<PaginatedResult<string>>;


