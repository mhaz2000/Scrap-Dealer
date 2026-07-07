using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class SaleOrderReadService : ISaleOrderReadService
    {
        private readonly DbSet<SaleOrderReadModel> _saleOrders;
        private readonly DbSet<ContractReadModel> _contracts;


        public SaleOrderReadService(ReadDbContext context)
        {
            _saleOrders = context.SaleOrders;
            _contracts = context.Contracts;
        }
        public async Task<int?> GetLastCodeAsync() => await _saleOrders.MaxAsync(s => (int?)s.Code);

        public async Task<bool> HasOngoingContractForSaleorderAsync(Guid sellerId)
        {
            return await _contracts.Include(t => t.SaleOrder).AnyAsync(t => t.SaleOrder.SellerId == sellerId &&
                !(t.Status == ContractStatus.Done || t.Status == ContractStatus.CancelledByBuyer || t.Status == ContractStatus.CancelledBySeller));
        }
    }
}
