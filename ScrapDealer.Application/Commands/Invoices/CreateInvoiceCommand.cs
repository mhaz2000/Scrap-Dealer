using ScrapDealer.Application.Commands.SaleOrders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Invoices;

public record CreateInvoiceCommand : ICommand
{
    public Guid? UserId { get; set; }
    public Guid ContractId { get; set; }
    public decimal Amount { get; set; }
    public IEnumerable<InvoiceItemCommand> Items { get; set; }
}


public record InvoiceItemCommand(Guid? SubCategoryId, SaleType Type, decimal amount, double weight);
