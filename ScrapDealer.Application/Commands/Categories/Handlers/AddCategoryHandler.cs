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
    internal class AddCategoryHandler : ICommandHandler<AddCategoryCommand>
    {
        private readonly ICategoryFactory _factory;
        private readonly ICategoryRepository _repository;
        private readonly ICategoryReadService _readService;
        private readonly IMediator _mediator;
        public AddCategoryHandler(ICategoryFactory factory, ICategoryRepository repository, ICategoryReadService readService, IMediator mediator)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByNameAsync(request.Name))
                throw new BusinessException("نام دسته بندی تکراری است.");

            var category = _factory.Create(request.Name, request.MinPrice, request.MaxPrice, request.Images);

            await _repository.AddAsync(category);

            await _mediator.Publish(new AddCategoryHistoryEvent(category.Id, request.MinPrice, request.MaxPrice, nameof(Category)));
        }
    }
}
