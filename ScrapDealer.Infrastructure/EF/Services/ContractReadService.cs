using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class ContractReadService : IContractReadService
    {
        private readonly DbSet<ContractReadModel> _contracts;
        public ContractReadService(ReadDbContext context)
        {
            _contracts = context.Contracts;
        }

        public async Task<bool> HasOngoingContractAsync(Guid buyerId)
        {
            return await _contracts.AnyAsync(t => !(t.Status == ContractStatus.Done || t.Status == ContractStatus.CancelledByBuyer || t.Status == ContractStatus.CancelledBySeller)
                && t.BuyerId == buyerId);
        }
    }
}
