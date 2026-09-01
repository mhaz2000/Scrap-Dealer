using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Invoices;

public class CreateInvoiceHandler(IInvoiceFactory factory, IInvoiceRepository invoiceRepository, IContractRepository contractRepository,
    ISubCategoryRepository subCategoryRepository, IInvoiceReadService invoiceReadService)
    : ICommandHandler<CreateInvoiceCommand>
{
    private const int invoiceCodeBase = 500000;
    public async Task Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetAsync(t => t.Id == request.ContractId && t.Buyer.UserId == request.UserId,
            t => t.Include(s => s.Buyer).Include(s => s.SaleOrder));

        if (contract is null)
            throw new BusinessException("قراردادی یافت نشد.");

        if(contract.Status == Domain.Consts.ContractStatus.CancelledByBuyer || contract.Status == Domain.Consts.ContractStatus.CancelledBySeller)
            throw new BusinessException("قرارداد لغو شده است.");

        if (contract.Status == Domain.Consts.ContractStatus.Done)
            throw new BusinessException("قراردادی پایان یافته است.");

        var existingInvoice = await invoiceRepository.GetAsync(t => t.ContractId == contract.Id);
        if (existingInvoice is not null)
            throw new BusinessException("قبلا برای این قرارداد فاکتور صادر شده است.");

        var lastCode = await invoiceReadService.GetLastCodeAsync();
        var nextCode = Code.Create(lastCode is null || lastCode < invoiceCodeBase ? invoiceCodeBase + 1 : lastCode.Value + 1);

        var invoice = factory.Create(contract, request.Amount, nextCode);

        foreach (var item in request.Items)
        {
            var category = await subCategoryRepository.GetAsync(c => c.Id == item.SubCategoryId);
            if (item.SubCategoryId is not null && category is null)
                throw new BusinessException("دسته بندی مورد نظر یافت نشد.");

            var invoiceItem = factory.CreateItem(category, item.Type, item.amount, item.weight);
            invoice.AddItem(invoiceItem);
        }

        contract.SaleOrder.Status = SaleOrderStatus.Done;
        contract.SetAmount(request.Amount);
        contract.SetStatus(ContractStatus.Done);

        await invoiceRepository.AddAsync(invoice);
        await invoiceRepository.CommitAsync();
    }
}