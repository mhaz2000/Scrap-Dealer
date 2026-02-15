using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    internal class DeleteSaleOrderHandler : ICommandHandler<DeleteSaleOrderCommand>
    {
        private readonly ISaleOrderRepository _repository;

        public DeleteSaleOrderHandler(ISaleOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSaleOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await _repository.GetAsync(c => c.Id == request.Id);
            if (saleOrder is null)
                throw new BusinessException("سفارش مورد نظر یافت نشد.");

            await _repository.DeleteAsync(saleOrder.Id);
        }
    }
}
