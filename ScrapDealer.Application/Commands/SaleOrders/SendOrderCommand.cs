using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record SendOrderCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
    }
}
