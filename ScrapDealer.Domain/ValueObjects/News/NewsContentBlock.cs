using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.News
{
    public class NewsContentBlock : ValueObject
    {
        public NewsContentBlockType Type { get; }
        public string Value { get; }

        public NewsContentBlock(NewsContentBlockType type, string value)
        {
            Type = type;
            Value = value;
        }

        public static NewsContentBlock CreateText(string text)
            => new NewsContentBlock(NewsContentBlockType.Text, text);

        public static NewsContentBlock CreateImage(Guid imageId)
            => new NewsContentBlock(NewsContentBlockType.Image, imageId.ToString());

        public static NewsContentBlock Create(NewsContentBlockType type, string value)
            => new NewsContentBlock(type, value);

        public override string ToString() => $"{Type}: {Value}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
        }
    }
}