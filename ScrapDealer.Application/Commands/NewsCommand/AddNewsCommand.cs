using ScrapDealer.Domain.ValueObjects.News;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.NewsCommand
{
    public record AddNewsCommand(string title, string summary, ICollection<NewsContentBlock> content) : ICommand;
}