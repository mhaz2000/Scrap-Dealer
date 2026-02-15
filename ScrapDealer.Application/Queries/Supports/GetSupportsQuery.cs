using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Supports;
public record GetSupportsQuery : PaginationQuery, IQuery<PaginatedResult<SupportDto>>;
