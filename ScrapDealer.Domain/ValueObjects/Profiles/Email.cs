using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email() { }
        private Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("ایمیل اجباری است.");

            value = value.Trim().ToLowerInvariant();

            if (!IsValidEmail(value))
                throw new BusinessException("فرمت ایمیل معتبر نیست.");

            Value = value;
        }

        public static Email Create(string email) => new Email(email);

        public static implicit operator string(Email email) => email.Value;

        public static implicit operator Email(string email) => new(email);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
