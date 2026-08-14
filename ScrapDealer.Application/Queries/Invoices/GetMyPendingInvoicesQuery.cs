using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Invoices;

public record GetMyPendingInvoicesQuery(Guid UserId) : PaginationQuery, IQuery<PaginatedResult<InvoiceListDto>>;
