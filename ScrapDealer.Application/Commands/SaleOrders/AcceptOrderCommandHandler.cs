using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public class AcceptOrderCommandHandler(IContractFactory factory, IContractRepository repository, IBuyerRepository buyerRepository, ISaleOrderRepository saleOrderRepository)
        : ICommandHandler<AcceptOrderCommand>
    {
        public async Task Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await saleOrderRepository.GetAsync(s => s.Id == request.Id);
            if (saleOrder is null || saleOrder.Status != Domain.Consts.SaleOrderStatus.ConfirmedBySystem)
                throw new BusinessException("سفارش فروش یافت نشد.");

            var buyer = await buyerRepository.GetAsync(c => c.UserId == request.UserId && c.IsActive);
            if (buyer is null)
                throw new BusinessException("خریدار یافت نشد.");

            var contract = factory.Create(saleOrder.Id, 0, 0, buyer.Id);
            await repository.AddAsync(contract);
        }
    }
}
