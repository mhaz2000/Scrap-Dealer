using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ICategoryPriceHistoryFactory
    {
        CategoryPriceHistory Create(decimal minPrice, decimal maxPrice, DateTime dateTime);
    }
}
