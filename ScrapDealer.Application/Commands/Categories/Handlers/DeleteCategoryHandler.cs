using MediatR;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMediator _mediator;
        public DeleteCategoryHandler(ICategoryRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(c => c.Id == request.Id, c => c.SubCategories);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            if (category.SubCategories.Any())
                throw new BusinessException("برای این دسته بندی، زیر مجموعه ایجاد شده است، ابتدا آن ها را پاک نمایید.");

            await _mediator.Publish(new DeleteCategoryOrSubCategoryEvent(request.Id));
            await _repository.DeleteAsync(request.Id);

        }
    }
}
