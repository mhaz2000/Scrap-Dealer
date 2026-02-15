using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories
{
    internal class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        public SellerRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
