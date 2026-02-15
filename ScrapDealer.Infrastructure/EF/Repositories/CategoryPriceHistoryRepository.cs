using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Repositories.Base;

namespace ScrapDealer.Infrastructure.EF.Repositories
{
    internal class CategoryPriceHistoryRepository : GenericRepository<CategoryPriceHistory>, ICategoryPriceHistoryRepository
    {
        public CategoryPriceHistoryRepository(WriteDbContext context) : base(context)
        {
        }

        public async Task RemoveHistoriesAsync(Guid id)
        {
            var toRemove = _context.CategoryPriceHistories.Where(t => t.CategoryId == id || t.SubCategoryId == id);
            _context.RemoveRange(toRemove);

            await _context.SaveChangesAsync();
        }
    }
}
