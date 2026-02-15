using MediatR;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class AddSubCategoryHandler : ICommandHandler<AddSubCategoryCommand>
    {
        private readonly ISubCategoryFactory _factory;
        private readonly ISubCategoryRepository _repository;
        private readonly ISubCategoryReadService _readService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _mediator;
        public AddSubCategoryHandler(ISubCategoryFactory factory, ISubCategoryRepository repository,
            ISubCategoryReadService readService, ICategoryRepository categoryRepository, IMediator mediator)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _categoryRepository = categoryRepository;
            _mediator = mediator;
        }

        public async Task Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == request.CategoryId);
            if(category is null)
                throw new BusinessException("دسته بندی مورد نظر وجود ندارد.");

            if (await _readService.ExistsByNameAsync(request.Name))
                throw new BusinessException("نام دسته بندی تکراری است.");

            var subCategory = _factory.Create(request.Name, request.minPrice, request.maxPrice, category, request.Images);

            await _repository.AddAsync(subCategory);

            await _mediator.Publish(new AddCategoryHistoryEvent(subCategory.Id, request.minPrice, request.maxPrice, nameof(SubCategory)));

        }
    }
}
