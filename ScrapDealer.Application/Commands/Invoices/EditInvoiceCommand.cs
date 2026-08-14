using ScrapDealer.Application.Commands.SaleOrders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Invoices;

public record EditInvoiceCommand : ICommand
{
    public Guid? UserId { get; set; }
    public Guid InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public IEnumerable<InvoiceItemCommand> Items { get; set; }
}
