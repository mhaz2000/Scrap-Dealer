using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.NewsCommand
{
    public record DeleteNewsCommand(Guid Id) : ICommand;
}
