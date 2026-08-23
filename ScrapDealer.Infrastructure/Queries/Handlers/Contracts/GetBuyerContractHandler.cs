using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Contracts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Contracts;

internal class GetBuyerContractHandler(ReadDbContext context, IMapper mapper) : IQueryHandler<GetBuyerContractQuery, BuyerContractDetailDto>
{
    private readonly DbSet<ContractReadModel> _contracts = context.Contracts;
    private readonly DbSet<ScoreHistoryReadModel> _scoreHistories = context.ScoreHistories;
    public async Task<BuyerContractDetailDto> Handle(GetBuyerContractQuery request, CancellationToken cancellationToken)
    {
        var contract = await _contracts.Include(t => t.SaleOrder).ThenInclude(t=> t.Seller).ThenInclude(t => t.User).FirstOrDefaultAsync(t => t.Id == request.Id);
        if (context is null)
            throw new BusinessException("قرارداد یافت نشد.");

        var data =  mapper.Map<BuyerContractDetailDto>(contract);
         var scoreHistory = await _scoreHistories.FirstOrDefaultAsync(t => t.ContractId == contract!.Id);

        data.ContractScore = scoreHistory?.Score;

        return data;
    }

}
