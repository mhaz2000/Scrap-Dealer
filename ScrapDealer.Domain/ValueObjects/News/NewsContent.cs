using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.News
{
    public class NewsContent : ValueObject
    {
        public string Value { get; }

        private NewsContent() { }
        private NewsContent(string value)
        {
            Value = value.Trim();
        }

        public static NewsContent Create(string value)
        {
            return new NewsContent(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            if (obj is NewsContent other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(NewsContent newsContent)
            => newsContent.Value;

        public static implicit operator NewsContent(string value)
            => Create(value);
    }
}
