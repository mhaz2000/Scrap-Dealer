using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISubCategoryFactory
    {
        SubCategory Create(CategoryName name, decimal minPrice, decimal maxPrice, Category category, ICollection<Guid> Images);
        SubCategory Update(CategoryName name, decimal minPrice, decimal maxPrice, SubCategory category, ICollection<Guid> Images);
    }
}
