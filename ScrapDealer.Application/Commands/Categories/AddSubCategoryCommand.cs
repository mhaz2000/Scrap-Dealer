using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record AddSubCategoryCommand(string Name, decimal minPrice, decimal maxPrice, Guid CategoryId, ICollection<Guid> Images) : ICommand;
}
