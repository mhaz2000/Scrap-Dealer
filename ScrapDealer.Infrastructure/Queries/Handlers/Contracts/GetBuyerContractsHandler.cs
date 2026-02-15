using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Contracts;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Contracts;
internal class GetBuyerContractsHandler : IQueryHandler<GetBuyerContractsQuery, PaginatedResult<BuyerContractDto>>
{
    private readonly DbSet<ContractReadModel> _contracts;
    private readonly IMapper _mapper;

    public GetBuyerContractsHandler(ReadDbContext context, IMapper mapper)
    {
        _contracts = context.Contracts;
        _mapper = mapper;
    }
    public async Task<PaginatedResult<BuyerContractDto>> Handle(GetBuyerContractsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _contracts.Include(c => c.SaleOrder)
            .ThenInclude(c => c.Seller).AsQueryable();

        if (query.IsOngoing is not null)
        {
            if (query.IsOngoing.Value)
                dbQuery = dbQuery.Where(t => t.Status == ContractStatus.AcceptByBuyer
                || t.Status == ContractStatus.PendingForCommission
                || t.Status == ContractStatus.AmountConfirmed
                || t.Status == ContractStatus.AdminConfirmed
                || t.Status == ContractStatus.AcceptBySeller);
            else
                dbQuery = dbQuery.Where(t => t.Status == ContractStatus.CancelledByBuyer
                || t.Status == ContractStatus.CancelledBySeller
                || t.Status == ContractStatus.Done);
        }

        var contracts = dbQuery.AsNoTracking();
        var paginatedResult = await contracts.
            ToPaginatedResultAsync<ContractReadModel, BuyerContractDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

        return paginatedResult;
    }
}
