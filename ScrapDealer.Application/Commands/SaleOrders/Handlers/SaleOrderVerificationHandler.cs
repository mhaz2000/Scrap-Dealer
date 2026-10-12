using ScrapDealer.Application.Commands.Notifications;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    public class SaleOrderVerificationHandler(ISaleOrderRepository repository, ICommandDispatcher commandDispatcher) : ICommandHandler<SaleOrderVerificationCommand>
    {
        public async Task Handle(SaleOrderVerificationCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await repository.GetWithSellerAsync(request.Id) ?? throw new BusinessException("دستور فروش یافت نشد.");

            saleOrder.UpdateStatus(request.verificationStatus ? SaleOrderStatus.ConfirmedBySystem : SaleOrderStatus.RejectedBySystem, request.rejectionReason);

            await repository.UpdateAsync(saleOrder);

            var title = request.verificationStatus ? "تایید سفارش فروش" : "رد سفارش فروش";
            var content = request.verificationStatus
                ? $"سفارش فروش شما به شماره {saleOrder.Code} تایید شد."
                : $"سفارش فروش به شماره {saleOrder.Code} به دلیل {request.rejectionReason} توسط سیستم رد شد.";

            await commandDispatcher.DispatchAsync(new AddNotificationCommand(
                title,
                content,
                IsPushEnabled: true,
                UserIds: [saleOrder.Seller.UserId]));
        }
    }
}
