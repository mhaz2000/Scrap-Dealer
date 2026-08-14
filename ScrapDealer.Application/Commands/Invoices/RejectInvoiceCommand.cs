using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Invoices;

public record RejectInvoiceCommand(Guid InvoiceId, Guid? UserId, string? Reason) : ICommand;
