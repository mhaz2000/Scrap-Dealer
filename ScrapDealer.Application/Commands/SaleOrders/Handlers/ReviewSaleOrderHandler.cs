using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    public class ReviewSaleOrderHandler(ISaleOrderRepository repository, ISaleOrderFactory factory, ISubCategoryRepository subCategoryRepository) 
        : ICommandHandler<ReviewSaleOrderCommand>
    {
        public async Task Handle(ReviewSaleOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await repository.GetAsync(t => t.Id == request.Id, t => t.Items);
            if (saleOrder is null)
                throw new BusinessException("دستور فروش یافت نشد.");

            foreach (var item in request.Items)
            {
                var saleOrderItem = saleOrder.Items.FirstOrDefault(t=> t.Id == item.Id) ?? throw new BusinessException("آیتم دستور فروش یافت نشد.");
                var subcategory = await subCategoryRepository.GetAsync(t => t.Id == item.SubCategoryId) ?? throw new BusinessException("دسته بندی یافت نشد.");

                factory.UpdateItem(subcategory, item.Description, item.SaleType, saleOrderItem, saleOrder);
            }


            await repository.UpdateAsync(saleOrder);
        }
    }
}
