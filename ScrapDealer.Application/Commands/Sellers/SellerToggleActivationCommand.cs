using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Sellers
{
    public record SellerToggleActivationCommand(Guid Id, bool Status) : ICommand;
}
