using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.InitialBuyers;

public record GetInitialBuyersQuery : PaginationQuery, IQuery<PaginatedResult<InitialBuyerDto>>;
