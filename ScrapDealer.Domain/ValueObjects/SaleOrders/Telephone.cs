using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Text.RegularExpressions;

namespace ScrapDealer.Domain.ValueObjects.SaleOrders
{
    public class Telephone : ValueObject
    {
        public string Value { get; }

        private Telephone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("شماره تلفن اجباری است.");

            if(!IsValidLandline(value))
                throw new BusinessException("فرمت شماره تلفن صحیح نیست.");

            Value = value.Trim();
        }

        public static Telephone Create(string value)
            => new Telephone(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public static bool IsValidLandline(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Normalize
            var cleaned = input.Trim()
                               .Replace(" ", "")
                               .Replace("-", "")
                               .Replace("(", "")
                               .Replace(")", "");

            // Iran landline:
            // 0 + (2 or 3 digit area code) + (7 or 8 digit number)
            return Regex.IsMatch(cleaned, @"^0\d{2,3}\d{7,8}$");
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Telephone address)
            => address.Value;

        public static implicit operator Telephone(string address)
            => new(address);
    }

}
