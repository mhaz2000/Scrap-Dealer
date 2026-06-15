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

internal class GetInvoicesHandler : IQueryHandler<GetInvoicesQuery, PaginatedResult<InvoiceListDto>>
{
    private readonly DbSet<InvoiceReadModel> _invoices;
    private readonly IMapper _mapper;

    public GetInvoicesHandler(ReadDbContext context, IMapper mapper)
    {
        _invoices = context.Invoices;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<InvoiceListDto>> Handle(GetInvoicesQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices
            .Include(s=> s.Contract).ThenInclude(s=> s.SaleOrder).ThenInclude(s=> s.Seller)
            .Include(s=> s.Contract).ThenInclude(s=> s.Buyer)
            .AsQueryable();

        if (!string.IsNullOrEmpty(query.Search))
            dbQuery = dbQuery
                .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Contract.SaleOrder.Seller.FirstName + " " + u.Contract.SaleOrder.Seller.LastName, $"%{query.Search}%") ||
                            Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Contract.Buyer.FirstName + " " + u.Contract.Buyer.LastName, $"%{query.Search}%"));

        var invoices = dbQuery.AsNoTracking();
        var paginatedResult = await invoices.
            ToPaginatedResultAsync<InvoiceReadModel, InvoiceListDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

        return paginatedResult;
    }
}
