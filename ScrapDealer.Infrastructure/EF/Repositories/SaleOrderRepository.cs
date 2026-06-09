using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories;

internal class SaleOrderRepository : GenericRepository<SaleOrder>, ISaleOrderRepository
{
    public SaleOrderRepository(WriteDbContext context) : base(context)
    {
    }

    public void ClearItems(SaleOrder saleOrder)
    {
        _context.SaleOrderItems.RemoveRange(saleOrder.Items);
    }
}
