using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.NewsCommand
{
    public record UpdateNewsCommand(string title, string summary, string content, Guid id) : ICommand;
}
