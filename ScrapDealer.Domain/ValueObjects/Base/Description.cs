namespace ScrapDealer.Domain.ValueObjects.Base
{
    public class Description : ValueObject
    {
        public string? Value { get; }

        private Description(string? value)
        {
            Value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static Description? Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new Description(null);

            return new Description(value);
        }

        public override string? ToString() => Value;

        public override bool Equals(object? obj)
        {
            if (obj is Description other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value?.GetHashCode() ?? 0;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string?(Description? description)
            => description?.Value;

        public static implicit operator Description?(string? value)
            => Create(value);
    }
}
