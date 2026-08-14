using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Invoices;

public class ApproveInvoiceHandler(IInvoiceRepository invoiceRepository)
    : ICommandHandler<ApproveInvoiceCommand>
{
    public async Task Handle(ApproveInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await invoiceRepository.GetAsync(
            t => t.Id == request.InvoiceId,
            t => t.Include(i => i.Contract).ThenInclude(c => c.SaleOrder).ThenInclude(s => s.Seller));

        if (invoice is null)
            throw new BusinessException("فاکتور یافت نشد.");

        if (invoice.Contract.SaleOrder.Seller.UserId != request.UserId)
            throw new BusinessException("شما فروشنده این قرارداد نیستید.");

        invoice.Approve();

        await invoiceRepository.UpdateAsync(invoice);
        await invoiceRepository.CommitAsync();
    }
}
