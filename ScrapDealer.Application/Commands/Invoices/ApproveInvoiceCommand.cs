using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Invoices;

public record ApproveInvoiceCommand(Guid InvoiceId, Guid? UserId) : ICommand;
