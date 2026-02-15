using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Buyers
{
    public record VerifyBuyerCommand(Guid Id) : ICommand;

}
