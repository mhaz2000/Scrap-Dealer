using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Commands.Invoices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Invoices;

public class EditInvoiceHandler(IInvoiceFactory factory, IInvoiceRepository invoiceRepository,
    ISubCategoryRepository subCategoryRepository)
    : ICommandHandler<EditInvoiceCommand>
{
    public async Task Handle(EditInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await invoiceRepository.GetAsync(
            t => t.Id == request.InvoiceId && t.Contract.Buyer.UserId == request.UserId,
            t => t.Include(i => i.Contract).ThenInclude(c => c.SaleOrder).ThenInclude(s => s.Seller)
                  .Include(i => i.Items));

        if (invoice is null)
            throw new BusinessException("فاکتور یافت نشد.");

        if (invoice.Status != Domain.Consts.InvoiceStatus.Rejected)
            throw new BusinessException("فاکتور در وضعیت قابل ویرایش نیست.");

        invoice.SetAmount(request.Amount);
        invoice.ClearItems();

        foreach (var item in request.Items)
        {
            var category = await subCategoryRepository.GetAsync(c => c.Id == item.SubCategoryId);
            if (item.SubCategoryId is not null && category is null)
                throw new BusinessException("دسته بندی مورد نظر یافت نشد.");

            var invoiceItem = factory.CreateItem(category, item.Type, item.amount, item.weight);
            invoice.AddItem(invoiceItem);
        }

        invoice.Resubmit();

        await invoiceRepository.UpdateAsync(invoice);
        await invoiceRepository.CommitAsync();
    }
}
