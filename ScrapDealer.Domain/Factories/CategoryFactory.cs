using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(string name, decimal minPrice, decimal maxPrice, ICollection<Guid> images)
        {
            var nameValue = CategoryName.Create(name);
            var priceValue = CategoryPriceRange.Create(minPrice, maxPrice);

            return new Category(nameValue, priceValue, images);
        }
        public Category Update(string name, decimal minPrice, decimal maxPrice, Category category, ICollection<Guid> images)
        {
            var nameValue = CategoryName.Create(name);
            var priceValue = CategoryPriceRange.Create(minPrice, maxPrice);

            category.Update(nameValue, priceValue, images);

            return category;
        }
    }
}
