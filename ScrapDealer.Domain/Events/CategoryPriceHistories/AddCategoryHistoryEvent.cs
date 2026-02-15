using MediatR;

namespace ScrapDealer.Domain.Events.CategoryPriceHistories
{
    public record AddCategoryHistoryEvent : INotification
    {
        public string CategoryType { get; set; }
        public Guid Id { get; }
        public decimal MinPrice { get; }
        public decimal MaxPrice { get; }

        public AddCategoryHistoryEvent(Guid id, decimal minPrice, decimal maxPrice, string categoryType)
        {
            Id = id;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            CategoryType = categoryType;
        }
    }
}
