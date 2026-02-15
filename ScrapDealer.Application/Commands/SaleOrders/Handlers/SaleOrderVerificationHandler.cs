using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    public class SaleOrderVerificationHandler(ISaleOrderRepository repository) : ICommandHandler<SaleOrderVerificationCommand>
    {
        public async Task Handle(SaleOrderVerificationCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await repository.GetAsync(t => t.Id == request.Id) ?? throw new BusinessException("دستور فروش یافت نشد.");

            saleOrder.UpdateStatus(request.verificationStatus ? SaleOrderStatus.ConfirmedBySystem : SaleOrderStatus.RejectedBySystem, request.rejectionReason);

            await repository.UpdateAsync(saleOrder);
        }
    }
}
