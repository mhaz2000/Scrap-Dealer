using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record CreateSaleOrderCommand(bool IsIndustrial, double? Latitude, double? Longitude, bool SaleAtBuyersLocation, string Address, string? Telephone,
        ICollection<SaleOrderItemCommand> Items, Guid UserId) : ICommand;
}
