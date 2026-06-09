using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    public class SendOrderHandler(ISaleOrderRequestFactory factory, ISaleOrderRequestRepository repository, IBuyerRepository buyerRepository,
        ISaleOrderRepository saleOrderRepository, ISaleOrderRequestRepository saleOrderRequestRepository)
        : ICommandHandler<SendOrderCommand>
    {
        public async Task Handle(SendOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await saleOrderRepository.GetAsync(s => s.Id == request.Id);
            if (saleOrder is null || saleOrder.Status != Domain.Consts.SaleOrderStatus.ConfirmedBySystem || !saleOrder.SaleAtBuyersLocation)
                throw new BusinessException("سفارش فروش یافت نشد.");

            var saleOrderRequest = await saleOrderRequestRepository.GetAsync(t => t.SaleOrderId == request.Id);
            if (saleOrderRequest is not null)
                throw new BusinessException("این سفارش قبلا برای خریداری ارسال شده است.");

            var buyer = await buyerRepository.GetAsync(c => (c.Id == request.BuyerId || c.UserId == request.BuyerId) && c.IsActive);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");


            var newSaleOrderRequest = factory.Create(buyer, saleOrder, DateTime.Now);
            await repository.AddAsync(newSaleOrderRequest);
            await repository.CommitAsync();
        }
    }
}
