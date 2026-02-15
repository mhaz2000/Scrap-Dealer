using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.NewsQueries;
public record GetNewsListQuery : PaginationQuery, IQuery<PaginatedResult<NewsDto>>;
