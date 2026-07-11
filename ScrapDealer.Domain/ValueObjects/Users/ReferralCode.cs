using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.Users
{
    public class ReferralCode : ValueObject
    {
        public string Value { get; }

        private ReferralCode() { }

        private ReferralCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("کد معرف نمی‌تواند خالی باشد.");

            Value = value;
        }

        public static ReferralCode Create(string value) => new ReferralCode(value);

        public static ReferralCode Generate()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(Enumerable.Repeat(chars, 12)
                .Select(c => c[random.Next(c.Length)]).ToArray());
            return new ReferralCode(code);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        public static implicit operator string(ReferralCode code) => code.Value;
    }
}
