using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    public class AcceptOrderHandler(IContractFactory factory, IContractRepository repository, ISaleOrderRequestRepository saleOrderRequestRepository,
        IBuyerRepository buyerRepository, ISaleOrderRepository saleOrderRepository)
        : ICommandHandler<AcceptOrderCommand>
    {
        public async Task Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await saleOrderRepository.GetAsync(s => s.Id == request.Id && !s.Seller.IsDeleted, t => t.Include(s => s.Seller));
            if (saleOrder is null)
                throw new BusinessException("سفارش فروش یافت نشد.");

            if(saleOrder.Status != SaleOrderStatus.ConfirmedBySystem)
                throw new BusinessException("سفارش هنوز توسط مدیر سیستم تایید نشده است.");

            var buyer = await buyerRepository.GetAsync(c => c.UserId == request.UserId && c.IsActive);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");

            var saleOrderRequest = await saleOrderRequestRepository.GetAsync(t => t.SaleOrderId == request.Id && t.BuyerId == buyer.Id);
            if (saleOrderRequest is not null)
                await saleOrderRequestRepository.DeleteAsync(saleOrderRequest.Id);

            var existingContract = await repository.GetAsync(t => t.SaleOrderId == saleOrder.Id);
            if(existingContract is not null && !(existingContract.Status == ContractStatus.CancelledByBuyer || existingContract.Status == ContractStatus.CancelledBySeller))
                throw new BusinessException("برای این سفارش فروش، قرارداد فعال وجود دارد.");

            saleOrder.Status = SaleOrderStatus.AcceptedByBuyer;

            var contract = factory.Create(saleOrder.Id, 0, 0, buyer.Id);
            await repository.AddAsync(contract);
            await repository.CommitAsync();

        }
    }
}
