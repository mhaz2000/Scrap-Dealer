using ScrapDealer.Domain.ValueObjects.Category;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class SubCategory : Entity<Guid>
    {
        public CategoryPriceRange PriceRange { get; private set; }
        public CategoryName Name { get; private set; }

        public Category Category { get; private set; }
        public Guid CategoryId { get; private set; }
        public bool IsDeleted { get; private set; }
        public ICollection<Guid> Images { get; private set; } = [];

        public SubCategory()
        {
            
        }

        public SubCategory(CategoryPriceRange priceRange, CategoryName name, Category category, ICollection<Guid> images)
        {
            PriceRange = priceRange;
            Name = name;
            Category = category; ;
            CategoryId = category.Id;
            Images = images;
        }

        internal void Update(CategoryPriceRange priceRange, CategoryName name, ICollection<Guid> images)
        {
            PriceRange = priceRange;
            Name = name;
            Images = images;
        }
    }
}
