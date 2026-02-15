using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Contracts;

public record GetSellerContractsQuery(bool? IsOngoing) : PaginationQuery, IQuery<PaginatedResult<SellerContractDto>>;
