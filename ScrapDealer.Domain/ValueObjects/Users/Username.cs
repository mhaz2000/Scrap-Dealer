using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Text.RegularExpressions;

namespace ScrapDealer.Domain.ValueObjects.Users
{
    public class Username : ValueObject
    {
        public string Value { get; }

        private Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام کاربری اجباری است.");

            if (value.Length < 4 || value.Length > 20)
                throw new BusinessException("نام کاربری باید بین 4 تا 20 کاراکتر باشد..");

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                throw new BusinessException("نام کاربری فقط می‌تواند شامل اعداد و حروف انگلیسی باشد.");

            Value = value;
        }

        public static Username Create(string username) => new Username(username);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(Username username)
            => username.Value;

        public static implicit operator Username(string username)
            => new(username);
    }

}
