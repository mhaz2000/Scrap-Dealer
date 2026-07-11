using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Referrals;

public record GetReferralsQuery : PaginationQuery, IQuery<PaginatedResult<ReferralDto>>;
