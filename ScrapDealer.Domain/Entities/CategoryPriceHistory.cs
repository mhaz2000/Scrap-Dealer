using ScrapDealer.Domain.ValueObjects.Category;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class CategoryPriceHistory : AggregateRoot<Guid>
    {
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Category? Category { get; set; }
        public CategoryPriceRange PriceRange { get; private set; }
        public DateTime DateTime { get; private set; }

        public CategoryPriceHistory()
        {
            
        }
        public CategoryPriceHistory(DateTime dateTime, CategoryPriceRange priceRange)
        {
            PriceRange = priceRange;
            DateTime = dateTime;
        }

    }
}
