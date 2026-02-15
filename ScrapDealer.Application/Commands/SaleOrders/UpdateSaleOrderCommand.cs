using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record UpdateSaleOrderCommand(bool IsIndustrial, double Latitude, double Longitude, string Address, string? Telephone,
        ICollection<SaleOrderItemCommand> Items, Guid Id) : ICommand;
}
