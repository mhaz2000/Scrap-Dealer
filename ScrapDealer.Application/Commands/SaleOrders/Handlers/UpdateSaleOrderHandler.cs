using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    internal class UpdateSaleOrderHandler : ICommandHandler<UpdateSaleOrderCommand>
    {
        private readonly ISaleOrderFactory _factory;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public UpdateSaleOrderHandler(ISaleOrderFactory factory, ISaleOrderRepository repository, ISubCategoryRepository subCategoryRepository)
        {
            _factory = factory;
            _saleOrderRepository = repository;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task Handle(UpdateSaleOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await _saleOrderRepository.GetAsync(t => t.Id == request.Id, t => t.Items);
            if (saleOrder is null)
                throw new BusinessException("دستور فروش یافت نشد.");

            _saleOrderRepository.ClearItems(saleOrder);

            foreach (var item in request.Items)
            {
                var category = await _subCategoryRepository.GetAsync(c => c.Id == item.SubCategoryId);
                if (item.SubCategoryId is not null && category is null)
                    throw new BusinessException("دسته بندی مورد نظر یافت نشد.");

                var saleItem = _factory.CreateItem(item.images, category, null, item.Description, item.Type);
                saleOrder.AddItem(saleItem);
            }

            _factory.Update(request.Address, request.Latitude, request.Longitude, request.Telephone, saleOrder);

            await _saleOrderRepository.UpdateAsync(saleOrder);
        }
    }
}
