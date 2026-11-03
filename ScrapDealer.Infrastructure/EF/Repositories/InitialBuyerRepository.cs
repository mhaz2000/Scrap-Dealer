using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Repositories.Base;
using ScrapDealer.Infrastructure.EF.Contexts;

namespace ScrapDealer.Infrastructure.EF.Repositories;

internal sealed class InitialBuyerRepository : GenericRepository<InitialBuyer>, IInitialBuyerRepository
{
    public InitialBuyerRepository(WriteDbContext context) : base(context) { }
}
