using ScrapDealer.Domain.ValueObjects.Category;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Domain;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.Entities
{
    public class Category : AggregateRoot<Guid>
    {
        public CategoryName Name { get; private set; }
        public CategoryPriceRange PriceRange { get; private set; }
        public DateTime LastUpdate { get; private set; }

        private readonly List<SubCategory> _subCategories = new List<SubCategory>();
        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories.AsReadOnly();
        public ICollection<Guid> Images { get; private set; } = [];

        public Category()
        {
            
        }
        public void AddSubCategory(SubCategory category)
        {
            if (category == null)
                throw new BusinessException("دسته بندی یافت نشد.");

            if (_subCategories.Any(r => r.CategoryId == category.Id))
                throw new BusinessException($"این نوع قبلا در دسته یندی اضافه شده است.");

            _subCategories.Add(category);
        }

        public Category(CategoryName name, CategoryPriceRange priceRange, ICollection<Guid> images)
        {
            Name = name;
            PriceRange = priceRange;
            Images = images;
            LastUpdate = DateTime.Now;
        }

        internal void Update(CategoryName name, CategoryPriceRange priceRange, ICollection<Guid> images)
        {
            Name = name;
            PriceRange = priceRange;
            Images = images;
            LastUpdate = DateTime.Now;
        }
    }
}
