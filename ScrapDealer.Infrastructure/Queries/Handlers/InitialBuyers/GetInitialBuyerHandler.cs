using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.InitialBuyers;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.InitialBuyers;

internal sealed class GetInitialBuyerHandler : IQueryHandler<GetInitialBuyerQuery, InitialBuyerDto?>
{
    private readonly DbSet<InitialBuyerReadModel> _buyers;
    private readonly IMapper _mapper;

    public GetInitialBuyerHandler(ReadDbContext context, IMapper mapper)
    {
        _buyers = context.InitialBuyers;
        _mapper = mapper;
    }

    public async Task<InitialBuyerDto?> Handle(GetInitialBuyerQuery query, CancellationToken cancellationToken)
    {
        var buyer = await _buyers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
        return buyer is null ? null : _mapper.Map<InitialBuyerDto>(buyer);
    }
}
