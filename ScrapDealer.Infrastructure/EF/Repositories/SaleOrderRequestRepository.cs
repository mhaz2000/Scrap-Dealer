using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories;

internal class SaleOrderRequestRepository : GenericRepository<SaleOrderRequest>, ISaleOrderRequestRepository
{
    public SaleOrderRequestRepository(WriteDbContext context) : base(context)
    {
    }
    }
