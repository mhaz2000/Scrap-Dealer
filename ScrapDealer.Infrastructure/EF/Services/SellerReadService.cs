using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal class SellerReadService : ISellerReadService
    {
        private readonly DbSet<SellerReadModel> _sellers;

        public SellerReadService(ReadDbContext context) => _sellers = context.Sellers;
        public async Task<bool> ExistsByUserIdAsync(Guid userId) => await _sellers.AnyAsync(b => b.UserId == userId);
    }
}
