using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.News
{
    public class NewsTitle : ValueObject
    {
        public string Value { get; }

        private NewsTitle() { }
        private NewsTitle(string value)
        {
            Value = value.Trim();
        }

        public static NewsTitle Create(string value)
        {
            return new NewsTitle(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            if (obj is NewsTitle other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(NewsTitle newsTitle)
            => newsTitle.Value;

        public static implicit operator NewsTitle(string value)
            => Create(value);
    }
}
