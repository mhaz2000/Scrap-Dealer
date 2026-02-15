using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record UpdateCategoryCommand(Guid Id, string Name, decimal MinPrice, decimal MaxPrice, ICollection<Guid> Images) : ICommand;
}
