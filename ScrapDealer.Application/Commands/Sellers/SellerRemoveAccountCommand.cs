using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Sellers
{
    public record SellerRemoveAccountCommand(Guid Id) : ICommand;
}
