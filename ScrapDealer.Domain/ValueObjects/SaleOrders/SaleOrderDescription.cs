using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.SaleOrders
{
    public class SaleOrderDescription : ValueObject
    {
        public string? Value { get; }

        private SaleOrderDescription(string? value)
        {
            Value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        public static SaleOrderDescription? Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new SaleOrderDescription(null);

            return new SaleOrderDescription(value);
        }

        public override string? ToString() => Value;

        public override bool Equals(object? obj)
        {
            if (obj is SaleOrderDescription other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value?.GetHashCode() ?? 0;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string?(SaleOrderDescription? description)
            => description?.Value;

        public static implicit operator SaleOrderDescription?(string? value)
            => Create(value);
    }
}
