using MediatR;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Repositories;

namespace ScrapDealer.Application.Events.CategoryPriceHistories
{
    public class DeleteCategoryOrSubCategoryEventHandler(ICategoryPriceHistoryRepository repository) : INotificationHandler<DeleteCategoryOrSubCategoryEvent>
    {
        public async Task Handle(DeleteCategoryOrSubCategoryEvent @event, CancellationToken cancellationToken)
            => await repository.RemoveHistoriesAsync(@event.Id);
        
    }
}
