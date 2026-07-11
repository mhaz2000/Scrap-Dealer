using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories
{
    internal sealed class ReferralRepository : GenericRepository<Referral>, IReferralRepository
    {
        public ReferralRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
