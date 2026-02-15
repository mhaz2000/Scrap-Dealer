using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Rewards;
public record GetRewardsQuery : PaginationQuery, IQuery<PaginatedResult<RewardDto>>;