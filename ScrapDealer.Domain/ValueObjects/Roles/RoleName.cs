using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Roles
{
    public class RoleName : ValueObject
    {
        public string Value { get; }

        private RoleName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام نقش نمی‌تواند خالی باشد.");

            Value = value;
        }

        public static RoleName Create(string roleName) => new RoleName(roleName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(RoleName roleName)
            => roleName.Value;

        public static implicit operator RoleName(string roleName)
            => new(roleName);
    }

}
