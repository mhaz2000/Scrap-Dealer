using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Roles
{
    public class PermissionName : ValueObject
    {
        public string Value { get; }

        private PermissionName() { }
        private PermissionName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("دسترسی نمی‌تواند خالی باشد.");

            Value = value;
        }

        public static PermissionName Create(string permissionName) => new PermissionName(permissionName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(PermissionName permissionName)
            => permissionName.Value;

        public static implicit operator PermissionName(string permissionName)
            => new(permissionName);
    }

}
