using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Contracts.Handlers;

internal class CancelContractCommandHandler(IContractRepository contractRepository) : ICommandHandler<CancelContractCommand>
{
    public async Task Handle(CancelContractCommand request, CancellationToken cancellationToken)
    {
        var contractByBuyer = await contractRepository.GetAsync(t => t.Buyer.UserId == request.UserId && t.Id == request.ContractId, t => t.Include(s => s.Buyer).Include(t=>t.SaleOrder));
        var contractBySeller = await contractRepository.GetAsync(t => t.SaleOrder.Seller.UserId == request.UserId && t.Id == request.ContractId,
            t => t.Include(s => s.SaleOrder).ThenInclude(t=> t.Seller));

        if (contractByBuyer is not null)
        {
            contractByBuyer.SetStatus(ContractStatus.CancelledByBuyer);
            contractByBuyer.SaleOrder.Status = SaleOrderStatus.ConfirmedBySystem;
        }
        else if (contractBySeller is not null)
        {
            contractBySeller.SetStatus(ContractStatus.CancelledByBuyer);
            contractBySeller.SaleOrder.Status = SaleOrderStatus.ConfirmedBySystem;
        }
        else
            throw new BusinessException("قرارداد یافت نشد.");
    }
}

