using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record CreateSaleOrderCommand(bool IsIndustrial, double Latitude, double Longitude, string Address, string? Telephone,
        ICollection<SaleOrderItemCommand> Items, Guid UserId) : ICommand;
}
