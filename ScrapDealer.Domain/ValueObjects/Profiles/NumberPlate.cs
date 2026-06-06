using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class NumberPlate : ValueObject
    {
        public string Value { get; }

        private NumberPlate() { }  
        private NumberPlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("پلاک ماشین اجباری است.");

            Value = value;
        }
        public static NumberPlate Create(string numberPlate) => new NumberPlate(numberPlate);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public static implicit operator string(NumberPlate numberPlate)
            => numberPlate.Value;

        public static implicit operator NumberPlate(string numberPlate)
            => new(numberPlate);

        //todo: validate the number plate

    }

}
