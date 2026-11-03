using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.InitialBuyers;

public record GetInitialBuyerQuery(Guid Id) : IQuery<InitialBuyerDto?>;
