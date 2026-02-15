using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record DeleteSaleOrderCommand(Guid Id) : ICommand;
}
