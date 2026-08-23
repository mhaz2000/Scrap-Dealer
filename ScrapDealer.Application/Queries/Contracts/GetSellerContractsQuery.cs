using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Contracts;

public record GetSellerContractsQuery(bool? IsOngoing, Guid? UserId) : PaginationQuery, IQuery<PaginatedResult<SellerContractDto>>;
