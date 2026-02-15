using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record AcceptOrderCommand(Guid Id, Guid UserId) : ICommand;
}
