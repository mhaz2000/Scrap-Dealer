using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Contracts;

public record GetSaleOrderByContractIdQuery(Guid Id) : IQuery<SaleOrderDto>;
