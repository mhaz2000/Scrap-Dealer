using MediatR;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Events.CategoryPriceHistories;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;

namespace ScrapDealer.Application.Events.CategoryPriceHistories
{
    public class AddCategoryHistoryEventHandler(ICategoryPriceHistoryFactory factory, ICategoryPriceHistoryRepository repository)
        : INotificationHandler<AddCategoryHistoryEvent>
    {
        public async Task Handle(AddCategoryHistoryEvent @event, CancellationToken cancellationToken)
        {
            var categoryPriceHistory = factory.Create(@event.MinPrice, @event.MaxPrice, DateTime.Now);

            if (@event.CategoryType == nameof(Category))
                categoryPriceHistory.CategoryId = @event.Id;
            else
                categoryPriceHistory.SubCategoryId = @event.Id;

            await repository.AddAsync(categoryPriceHistory);
        }
    }
}
