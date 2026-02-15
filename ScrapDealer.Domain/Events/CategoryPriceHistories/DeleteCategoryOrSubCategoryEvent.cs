using MediatR;

namespace ScrapDealer.Domain.Events.CategoryPriceHistories
{
    public record DeleteCategoryOrSubCategoryEvent(Guid Id) : INotification;
}
