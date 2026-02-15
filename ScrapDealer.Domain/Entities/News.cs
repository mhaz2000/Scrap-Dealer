using ScrapDealer.Domain.ValueObjects.News;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class News : AggregateRoot<Guid>
    {
        public NewsTitle Title { get; private set; }
        public NewsSummary Summary { get; private set; }
        public NewsContent Content { get; private set; }

        public News()
        {
            
        }
        public News(NewsTitle newsTitle, NewsSummary newsSummary, NewsContent newsContent)
        {
            Title = newsTitle;
            Summary = newsSummary;
            Content = newsContent;
        }

        public void Update(NewsTitle newsTitle, NewsSummary newsSummary, NewsContent newsContent)
        {
            Title = newsTitle;
            Summary = newsSummary;
            Content = newsContent;
        }
    }
}
