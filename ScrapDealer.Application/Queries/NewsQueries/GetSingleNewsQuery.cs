using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.NewsQueries;

public record GetSingleNewsQuery(Guid Id) : IQuery<NewsDto>;
