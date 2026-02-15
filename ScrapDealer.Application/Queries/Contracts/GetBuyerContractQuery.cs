using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Contracts;

public record GetBuyerContractQuery(Guid Id) : IQuery<BuyerContractDetailDto>;
