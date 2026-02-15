using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrapDealer.Domain.ValueObjects.Users
{
    public class Phone : ValueObject
    {
        private static readonly Regex IranPhoneRegex = new(@"^(?:\+98|0)?9\d{9}$", RegexOptions.Compiled);

        public string Value { get; }

        private Phone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("فرمت شماره تلفن اشتباه است.");

            if (!IranPhoneRegex.IsMatch(value))
                throw new BusinessException("فرمت شماره تلفن اشتباه است.");

            Value = Normalize(value);
        }

        public static Phone Create(string value) => new(value);

        private static string Normalize(string phone)
        {
            if (phone.StartsWith("+98"))
                phone = $"0{phone[1..]}";

            return phone.Replace("+98", "");
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
        {
            if (obj is Phone other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Phone phone)
            => phone.Value;

        public static implicit operator Phone(string phone)
            => new(phone);
    }
}
