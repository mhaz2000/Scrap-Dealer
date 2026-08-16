using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.News;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface INewsFactory
    {
        News Create(NewsTitle newsTitle, NewsSummary newsSummary, ICollection<NewsContentBlock> contentBlocks);
        News Update(NewsTitle newsTitle, NewsSummary newsSummary, ICollection<NewsContentBlock> contentBlocks, News news);
    }
}