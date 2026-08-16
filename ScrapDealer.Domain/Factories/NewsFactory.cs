using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.News;

namespace ScrapDealer.Domain.Factories
{
    public class NewsFactory : INewsFactory
    {
        public News Create(NewsTitle newsTitle, NewsSummary newsSummary, ICollection<NewsContentBlock> contentBlocks)
        {
            var newsTitleValue = NewsTitle.Create(newsTitle);
            var newsContentValue = NewsContent.Create(contentBlocks);
            var newsSummaryValue = NewsSummary.Create(newsSummary);

            return new News(newsTitleValue, newsSummaryValue, newsContentValue);
        }

        public News Update(NewsTitle newsTitle, NewsSummary newsSummary, ICollection<NewsContentBlock> contentBlocks, News news)
        {
            var newsTitleValue = NewsTitle.Create(newsTitle);
            var newsSummaryValue = NewsSummary.Create(newsSummary);
            var newsContentValue = NewsContent.Create(contentBlocks);

            news.Update(newsTitleValue, newsSummaryValue, newsContentValue);
            return news;
        }
    }
}