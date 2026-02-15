using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class SaleOrderReadService : ISaleOrderReadService
    {
        private readonly DbSet<SaleOrderReadModel> _saleOrders;

        public SaleOrderReadService(ReadDbContext context) => _saleOrders = context.SaleOrders;
    }
}
