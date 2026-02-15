using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record SaleOrderVerificationCommand(Guid Id, bool verificationStatus, string? rejectionReason) : ICommand;
}
