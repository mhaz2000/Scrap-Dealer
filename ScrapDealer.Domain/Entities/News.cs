using ScrapDealer.Domain.ValueObjects.News;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class News : AggregateRoot<Guid>
    {
        public NewsTitle Title { get; private set; }
        public NewsSummary Summary { get; private set; }
        public NewsContent Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public News()
        {
            
        }
        public News(NewsTitle newsTitle, NewsSummary newsSummary, NewsContent newsContent)
        {
            Title = newsTitle;
            Summary = newsSummary;
            Content = newsContent;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void Update(NewsTitle newsTitle, NewsSummary newsSummary, NewsContent newsContent)
        {
            Title = newsTitle;
            Summary = newsSummary;
            Content = newsContent;
            UpdatedAt = DateTime.Now;
        }
    }
}
