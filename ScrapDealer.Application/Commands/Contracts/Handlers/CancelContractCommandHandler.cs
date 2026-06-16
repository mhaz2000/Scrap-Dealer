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
        var contractByBuyer = await contractRepository.GetAsync(t => t.BuyerId == request.UserId);
        var contractBySeller = await contractRepository.GetAsync(t => t.SaleOrder.SellerId == request.UserId, t => t.Include(s => s.SaleOrder));

        if (contractByBuyer is not null)
        {
            contractByBuyer.SetStatus(ContractStatus.CancelledByBuyer);
        }
        else if (contractBySeller is not null)
        {
            contractBySeller.SetStatus(ContractStatus.CancelledByBuyer);
        }
        else
            throw new BusinessException("قرارداد یافت نشد.");
    }
}

