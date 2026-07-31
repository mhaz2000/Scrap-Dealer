using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Invoices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Invoices;

internal class GetInvoiceByIdHandler : IQueryHandler<GetInvoiceByIdQuery, InvoiceDto>
{
    private readonly DbSet<InvoiceReadModel> _invoices;
    private readonly IMapper _mapper;

    public GetInvoiceByIdHandler(ReadDbContext context, IMapper mapper)
    {
        _invoices = context.Invoices;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices
            .Include(s => s.Contract).ThenInclude(s => s.SaleOrder).ThenInclude(s => s.Seller)
            .Include(s => s.Contract).ThenInclude(s => s.Buyer)
            .Include(s=> s.Items).ThenInclude(t=>t.SubCategory).ThenInclude(s=>s.Category)
            .AsQueryable();

        var invoice = await dbQuery.FirstOrDefaultAsync(t => t.Id == query.Id);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
