using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record AddCategoryCommand(string Name, decimal MinPrice, decimal MaxPrice, ICollection<Guid> Images) : ICommand;
}
