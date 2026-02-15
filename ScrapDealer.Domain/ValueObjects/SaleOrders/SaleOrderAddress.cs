using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.SaleOrders
{
    public class SaleOrderAddress : ValueObject
    {
        public string Value { get; }

        private SaleOrderAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("آدرس کامل اجباری است.");

            Value = value.Trim();
        }

        public static SaleOrderAddress Create(string value)
            => new SaleOrderAddress(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(SaleOrderAddress address)
            => address.Value;

        public static implicit operator SaleOrderAddress(string address)
            => new(address);
    }

}
