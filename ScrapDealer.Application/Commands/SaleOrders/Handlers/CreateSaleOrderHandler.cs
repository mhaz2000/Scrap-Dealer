using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    internal class CreateSaleOrderHandler : ICommandHandler<CreateSaleOrderCommand>
    {
        private readonly ISaleOrderFactory _factory;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISellerRepository _sellerRepository;

        public CreateSaleOrderHandler(ISaleOrderFactory factory, ISaleOrderRepository repository,
            ISubCategoryRepository subCategoryRepository, ISellerRepository sellerRepository)
        {
            _factory = factory;
            _saleOrderRepository = repository;
            _sellerRepository = sellerRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task Handle(CreateSaleOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Items.Distinct().Count() != request.Items.Count())
                throw new BusinessException("دسته بندی تکراری است.");

            var seller = await _sellerRepository.GetAsync(c => c.UserId == request.UserId);
            if (seller is null)
                throw new BusinessException("فروشنده یافت نشد.");

            if (!seller.Verified)
                throw new BusinessException("ابتدا فرایند احراز هویت خود را تکمیل کنید.");

            var saleOrder = _factory.Create(request.IsIndustrial, seller, request.Address, request.Latitude, request.Longitude, request.Telephone);

            foreach (var item in request.Items)
            {
                var category = await _subCategoryRepository.GetAsync(c => c.Id == item.SubCategoryId);
                if (item.SubCategoryId is not null && category is null)
                    throw new BusinessException("دسته بندی مورد نظر یافت نشد.");

                var saleItem = _factory.CreateItem(item.images, category, null, item.Description, item.Type);
                saleOrder.AddItem(saleItem);
            }

            await _saleOrderRepository.AddAsync(saleOrder);
        }
    }
}
