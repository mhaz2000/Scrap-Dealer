using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories.Base;

namespace ScrapDealer.Domain.Repositories
{
    public interface ICategoryPriceHistoryRepository : IGenericRepository<CategoryPriceHistory>
    {
        Task RemoveHistoriesAsync(Guid id);
    }
}
