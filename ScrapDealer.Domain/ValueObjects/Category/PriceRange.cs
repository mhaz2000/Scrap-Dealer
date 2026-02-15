using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Category
{
    public class CategoryPriceRange : ValueObject
    {
        public decimal MinValue { get; }
        public decimal MaxValue { get; }

        private CategoryPriceRange(decimal minValue, decimal maxValue)
        {
            if (minValue < 0 || maxValue < 0)
                throw new BusinessException("مبالغ نمی‌توانند منفی باشند.");

            if (minValue > maxValue)
                throw new BusinessException("حداقل مقدار نمی‌تواند بزرگ‌تر از حداکثر مقدار باشد.");

            MinValue = minValue;
            MaxValue = maxValue;
        }

        public static CategoryPriceRange Create(decimal minValue, decimal maxValue)
            => new(minValue, maxValue);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MinValue;
            yield return MaxValue;
        }

        // Optional: check if a price is inside the range
        public bool Contains(decimal price)
            => price >= MinValue && price <= MaxValue;
    }

}
