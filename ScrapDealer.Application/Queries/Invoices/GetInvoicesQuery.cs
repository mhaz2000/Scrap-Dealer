using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Invoices;
public record GetInvoicesQuery : PaginationQuery, IQuery<PaginatedResult<InvoiceListDto>>;
