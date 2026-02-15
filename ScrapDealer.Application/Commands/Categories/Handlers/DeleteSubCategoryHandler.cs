using MediatR;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class DeleteSubCategoryHandler : ICommandHandler<DeleteSubCategoryCommand>
    {
        private readonly ISubCategoryRepository _repository;
        private readonly IMediator _mediator;

        public DeleteSubCategoryHandler(ISubCategoryRepository repository, IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _repository.GetAsync(c => c.Id == request.Id);
            if (subCategory is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            await _mediator.Publish(new DeleteCategoryOrSubCategoryEvent(request.Id));
            await _repository.DeleteAsync(request.Id);

        }
    }
}
