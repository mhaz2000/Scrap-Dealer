using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Settings
{
    public class CommissionRate : ValueObject
    {
        public float Value { get; }

        private CommissionRate() { }
        private CommissionRate(float value)
        {
            if (value < 0 || value >100)
                throw new BusinessException("نرخ کمیسیون باید بین 0 تا 100 باشد.");

            Value = value;
        }

        public static CommissionRate Create(float value)
        {
            return new CommissionRate(value);
        }

        public override bool Equals(object obj)
        {
            if (obj is CommissionRate other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator float(CommissionRate commissionRate)
            => commissionRate.Value;

        public static implicit operator CommissionRate(float value)
            => Create(value);
    }

}
