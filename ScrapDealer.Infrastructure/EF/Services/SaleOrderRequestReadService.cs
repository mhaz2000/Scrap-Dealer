using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class SaleOrderRequestReadService : ISaleOrderRequestReadService
    {
        private readonly DbSet<SaleOrderRequestReadModel> _saleOrderRequests;
        public SaleOrderRequestReadService(ReadDbContext context)
        {
            _saleOrderRequests = context.SaleOrderRequests;
        }

        public async Task<bool> HasOngoingSaleOrderRequest(Guid sellerId)
        {
            return await _saleOrderRequests.AnyAsync(t => t.SaleOrder.SellerId == sellerId);
        }
    }
}
