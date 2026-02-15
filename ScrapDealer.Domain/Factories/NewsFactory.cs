using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.News;

namespace ScrapDealer.Domain.Factories
{
    public class NewsFactory : INewsFactory
    {
        public News Create(NewsTitle newsTitle, NewsSummary newsSummary, NewsContent newsContent)
        {
            var newsTitleValue = NewsTitle.Create(newsTitle);
            var newsContentValue = NewsContent.Create(newsContent);
            var newsSummaryValue = NewsSummary.Create(newsSummary);

            return new News(newsTitleValue, newsSummaryValue, newsContentValue);
        }

        public News Update(NewsTitle newsTitle, NewsSummary newsSummary, NewsContent newsContent, News news)
        {
            var newsTitleValue = NewsTitle.Create(newsTitle);
            var newsSummaryValue = NewsSummary.Create(newsSummary);
            var newsContentValue = NewsContent.Create(newsContent);

            news.Update(newsTitleValue, newsSummaryValue, newsContentValue);
            return news;
        }
    }
}
