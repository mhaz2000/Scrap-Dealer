using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories.Base;

namespace ScrapDealer.Domain.Repositories
{
    public interface ISaleOrderRepository : IGenericRepository<SaleOrder>
    {
        void ClearItems(SaleOrder saleOrder);
    }
}
