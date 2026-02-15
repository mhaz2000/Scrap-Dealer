using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Category
{
    public class CategoryName : ValueObject
    {
        public string Value { get; set; }

        private CategoryName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام اجباری است.");

            Value = value.Trim();
        }

        public static CategoryName Create(string value)
            => new CategoryName(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }

        public static implicit operator string(CategoryName username)
      => username.Value;

        public static implicit operator CategoryName(string username)
            => new(username);
    }
}
