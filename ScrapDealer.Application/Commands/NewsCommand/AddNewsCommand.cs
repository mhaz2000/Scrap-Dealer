using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.NewsCommand
{
    public record AddNewsCommand(string title, string summary, string content) : ICommand;
}
