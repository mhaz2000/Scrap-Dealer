using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Invoices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Invoices;

internal class GetMyInvoicesHandler : IQueryHandler<GetMyInvoicesQuery, PaginatedResult<InvoiceListDto>>
{
    private readonly DbSet<InvoiceReadModel> _invoices;
    private readonly IMapper _mapper;

    public GetMyInvoicesHandler(ReadDbContext context, IMapper mapper)
    {
        _invoices = context.Invoices;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<InvoiceListDto>> Handle(GetMyInvoicesQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices
            .Include(s => s.Contract).ThenInclude(s => s.SaleOrder).ThenInclude(s => s.Seller)
            .Include(s => s.Contract).ThenInclude(s => s.Buyer)
            .AsQueryable();

        dbQuery = dbQuery
            .Where(u => u.Contract.Buyer.UserId == query.UserId || u.Contract.SaleOrder.Seller.UserId == query.UserId);

        var invoices = dbQuery.AsNoTracking();
        var paginatedResult = await invoices.
            ToPaginatedResultAsync<InvoiceReadModel, InvoiceListDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

        return paginatedResult;
    }
}
