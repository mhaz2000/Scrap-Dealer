using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Contracts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Contracts;

internal class GetSellerContractHandler(ReadDbContext context, IMapper mapper) : IQueryHandler<GetSellerContractQuery, SellerContractDetailDto>
{
    private readonly DbSet<ContractReadModel> _contracts = context.Contracts;
    public async Task<SellerContractDetailDto> Handle(GetSellerContractQuery request, CancellationToken cancellationToken)
    {
        var contract = await _contracts.Include(t => t.Buyer).ThenInclude(t=> t.User).FirstOrDefaultAsync(t => t.Id == request.Id);
        if (context is null)
            throw new BusinessException("قرارداد یافت نشد.");

        return mapper.Map<SellerContractDetailDto>(contract);
    }
}
