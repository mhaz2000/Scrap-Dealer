using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class UpdateSubCategoryHandler : ICommandHandler<UpdateSubCategoryCommand>
    {
        private readonly ISubCategoryFactory _factory;
        private readonly ISubCategoryRepository _repository;
        private readonly ICategoryReadService _readService;
        private readonly IMediator _mediator;

        public UpdateSubCategoryHandler(ISubCategoryFactory factory, ISubCategoryRepository repository, ICategoryReadService readService, IMediator mediator)
        {
            _repository = repository;
            _factory = factory;
            _readService = readService;
            _mediator = mediator;
        }
        public async Task Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(c => c.Id == request.Id);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            if (await _readService.ExistsByNameAsync(request.Name, request.Id))
                throw new BusinessException("عنوان دسته بندی تکراری است.");

            _factory.Update(request.Name, request.minPrice, request.maxPrice, category, request.Images);
            await _repository.UpdateAsync(category);

            await _mediator.Publish(new AddCategoryHistoryEvent(category.Id, request.minPrice, request.maxPrice, nameof(SubCategory)));

        }
    }
}