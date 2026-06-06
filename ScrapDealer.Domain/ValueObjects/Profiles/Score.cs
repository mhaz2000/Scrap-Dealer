using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class Score : ValueObject
    {
        public float Value { get; }

        private Score() { }
        private Score(float value)
        {
            if (value < 0)
                throw new BusinessException("امتیاز نمی تواند کمتر از 0 باشد.");

            Value = value;
        }

        public static Score Create(float score) => new Score(score);

        public static implicit operator float(Score score) => score.Value;

        public static implicit operator Score(float score) => new(score);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
