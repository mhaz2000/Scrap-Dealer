using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record UpdateSubCategoryCommand(Guid Id, string Name, decimal minPrice, decimal maxPrice, ICollection<Guid> Images) : ICommand;
}
