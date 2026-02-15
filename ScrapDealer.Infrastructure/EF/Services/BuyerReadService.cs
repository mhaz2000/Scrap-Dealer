using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class BuyerReadService : IBuyerReadService
    {
        private readonly DbSet<BuyerReadModel> _buyers;

        public BuyerReadService(ReadDbContext context) => _buyers = context.Buyers;
        public async Task<bool> ExistsByUserIdAsync(Guid userId) => await _buyers.AnyAsync(b => b.UserId == userId);
    }
}
