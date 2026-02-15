using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record DeleteSubCategoryCommand(Guid Id) : ICommand;
}
