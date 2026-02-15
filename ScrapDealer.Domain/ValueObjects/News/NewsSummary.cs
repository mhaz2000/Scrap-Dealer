using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.News
{
    public class NewsSummary : ValueObject
    {
        public string Value { get; }

        private NewsSummary(string value)
        {
            Value = value.Trim();
        }

        public static NewsSummary Create(string value)
        {
            return new NewsSummary(value);
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

        public static implicit operator string(NewsSummary newsTitle)
            => newsTitle.Value;

        public static implicit operator NewsSummary(string value)
            => Create(value);
    }
}
