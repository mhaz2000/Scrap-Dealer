using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record ReviewSaleOrderCommand(Guid Id, IEnumerable<ReviewSaleOrderItemCommand> Items) : ICommand;
}
