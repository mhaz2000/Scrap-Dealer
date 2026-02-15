using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class CompanyName : ValueObject
    {
        public string Value { get; }

        private CompanyName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام شرکت اجباری است.");

            Value = value;
        }
        public static CompanyName Create(string companyName) => new CompanyName(companyName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public static implicit operator string(CompanyName companyName)
            => companyName.Value;

        public static implicit operator CompanyName(string companyName)
            => new(companyName);


    }

}
