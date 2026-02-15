using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ICategoryFactory
    {
        Category Create(string name, decimal minPrice, decimal maxPrice, ICollection<Guid> images);
        Category Update(string name, decimal minPrice, decimal maxPrice, Category category, ICollection<Guid> images);
    }
}
