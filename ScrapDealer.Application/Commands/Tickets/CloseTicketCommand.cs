using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Tickets
{
    public record CloseTicketCommand(Guid Id) : ICommand;
}
