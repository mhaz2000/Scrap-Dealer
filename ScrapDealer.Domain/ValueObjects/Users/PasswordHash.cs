using Microsoft.AspNetCore.Identity;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Users
{
    public class PasswordHash : ValueObject
    {
        public string Value { get; }
        private static readonly Regex PasswordRegex = new(@"^(?=.*[A-Za-z])(?=.*\d).{8,}$", RegexOptions.Compiled);

        private PasswordHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("رمز عبور نمی‌تواند خالی باشد.");

            Value = value;
        }

        public static PasswordHash Create(string plainPassword, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new BusinessException("رمز عبور نمی‌تواند خالی باشد.");

            if (!PasswordRegex.IsMatch(plainPassword))
                throw new BusinessException("فرمت رمز باید 8 رقم و شامل اعداد و حروف انگلیسی باشد.");

            string hashedPassword = passwordHasher.HashPassword(null, plainPassword);
            return new PasswordHash(hashedPassword);
        }

        public static PasswordHash FromHash(string existingHash)
        {
            return new PasswordHash(existingHash);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
