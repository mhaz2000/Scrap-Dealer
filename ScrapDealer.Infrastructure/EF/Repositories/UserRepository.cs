using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories
{
    internal sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
