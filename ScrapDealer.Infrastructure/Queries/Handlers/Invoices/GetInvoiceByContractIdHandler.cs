using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Invoices;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Invoices;

internal class GetInvoiceByContractIdHandler : IQueryHandler<GetInvoiceByContractIdQuery, InvoiceDto>
{
    private readonly DbSet<InvoiceReadModel> _invoices;
    private readonly IMapper _mapper;

    public GetInvoiceByContractIdHandler(ReadDbContext context, IMapper mapper)
    {
        _invoices = context.Invoices;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(GetInvoiceByContractIdQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = _invoices
            .Include(s => s.Contract).ThenInclude(s => s.SaleOrder).ThenInclude(s => s.Seller)
            .Include(s => s.Contract).ThenInclude(s => s.Buyer)
            .Include(s => s.Items).ThenInclude(t => t.SubCategory).ThenInclude(s => s.Category)
            .AsQueryable();

        var invoice = await dbQuery.FirstOrDefaultAsync(t => t.ContractId == query.Id);
        if (invoice is null)
            throw new BusinessException("برای این قرار داد فاکتوری ثبت نشده است.");


        return _mapper.Map<InvoiceDto>(invoice);
    }
}
