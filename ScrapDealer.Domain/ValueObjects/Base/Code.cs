using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Base
{
    public class Code : ValueObject
    {
        public int Value { get; }

        private Code() { }
        private Code(int value)
        {
            if (value <= 0)
                throw new BusinessException("کد باید بزرگتر از صفر باشد.");
            Value = value;
        }

        public static Code Create(int value) => new Code(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(Code code) => code.Value;
        public static implicit operator Code(int value) => Create(value);

        public override string ToString() => Value.ToString();
    }
}
