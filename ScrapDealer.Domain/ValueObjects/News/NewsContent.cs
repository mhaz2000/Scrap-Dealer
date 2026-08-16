using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.News
{
    public class NewsContent : ValueObject
    {
        public IReadOnlyCollection<NewsContentBlock> Blocks { get; }

        private NewsContent() { }
        private NewsContent(IEnumerable<NewsContentBlock> blocks)
        {
            Blocks = blocks?.ToList() ?? new List<NewsContentBlock>();
        }

        public static NewsContent Create(IEnumerable<NewsContentBlock> blocks)
        {
            return new NewsContent(blocks);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var block in Blocks)
                yield return block;
        }
    }
}