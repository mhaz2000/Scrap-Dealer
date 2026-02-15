using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record DeleteCategoryCommand(Guid Id) : ICommand;
}
