using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Buyers
{
    public record BuyerToggleActivationCommand(Guid Id, bool Status) : ICommand;

}
