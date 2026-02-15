using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories
{
    public class SubCategoryFactory : ISubCategoryFactory
    {
        public SubCategory Create(CategoryName name, decimal minPrice, decimal maxPrice, Category category, ICollection<Guid> images)
        {
            var nameValue = CategoryName.Create(name);
            var priceValue = CategoryPriceRange.Create(minPrice, maxPrice);

            var subCategory = new SubCategory(priceValue, name, category, images);
            category.AddSubCategory(subCategory);
            return subCategory;
        }

        public SubCategory Update(CategoryName name, decimal minPrice, decimal maxPrice, SubCategory subCategory, ICollection<Guid> images)
        {
            var nameValue = CategoryName.Create(name);
            var priceValue = CategoryPriceRange.Create(minPrice, maxPrice);

            subCategory.Update(priceValue, nameValue, images);
            return subCategory;
        }
    }
}
