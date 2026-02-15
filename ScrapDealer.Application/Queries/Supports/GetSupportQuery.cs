using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Supports;

public record GetSupportQuery(Guid Id) : IQuery<SupportDto>;
