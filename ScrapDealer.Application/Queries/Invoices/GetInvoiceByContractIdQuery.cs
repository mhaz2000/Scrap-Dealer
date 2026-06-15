using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Invoices;

public record GetInvoiceByContractIdQuery(Guid Id) : IQuery<InvoiceDto>;
