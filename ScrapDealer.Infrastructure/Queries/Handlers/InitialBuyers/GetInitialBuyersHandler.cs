using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.InitialBuyers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.InitialBuyers;

internal sealed class GetInitialBuyersHandler : IQueryHandler<GetInitialBuyersQuery, PaginatedResult<InitialBuyerDto>>
{
    private readonly DbSet<InitialBuyerReadModel> _buyers;
    private readonly IMapper _mapper;

    public GetInitialBuyersHandler(ReadDbContext context, IMapper mapper)
    {
        _buyers = context.InitialBuyers;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<InitialBuyerDto>> Handle(GetInitialBuyersQuery query, CancellationToken cancellationToken)
        => await _buyers.AsNoTracking().ToPaginatedResultAsync<InitialBuyerReadModel, InitialBuyerDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);
}
