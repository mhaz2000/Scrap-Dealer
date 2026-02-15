using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories
{
    public class CategoryPriceHistoryFactory : ICategoryPriceHistoryFactory
    {
        public CategoryPriceHistory Create(decimal minPrice, decimal maxPrice, DateTime dateTime)
        {
            var priceValue = CategoryPriceRange.Create(minPrice, maxPrice);

            return new CategoryPriceHistory(dateTime, priceValue);
        }
    }
}
