using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class NationalCode : ValueObject
    {
        public string Value { get; }

        private NationalCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("کد ملی اجباری است.");

            if(!IsValidIranianNationalCode(value))
                throw new BusinessException("فرمت کد ملی صحیح نیست.");

            Value = value;
        }
        public static NationalCode Create(string nationalCode) => new NationalCode(nationalCode);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public static implicit operator string(NationalCode nationalCode)
            => nationalCode.Value;

        public static implicit operator NationalCode(string nationalCode)
            => new(nationalCode);

        private bool IsValidIranianNationalCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != 10)
                return false;

            if (!System.Text.RegularExpressions.Regex.IsMatch(code, @"^\d{10}$"))
                return false;

            if (new string(code[0], 10) == code)
                return false;

            int check = int.Parse(code[9].ToString());
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(code[i].ToString()) * (10 - i);
            }

            int remainder = sum % 11;

            return (remainder < 2 && check == remainder) || (remainder >= 2 && check == (11 - remainder));
        }

    }

}
