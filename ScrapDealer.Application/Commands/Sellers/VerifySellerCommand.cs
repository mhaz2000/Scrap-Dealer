using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Sellers
{
    public record VerifySellerCommand(Guid Id) : ICommand;
}
