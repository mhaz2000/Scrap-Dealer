using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        private PersonName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new BusinessException("نام اجباری است.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new BusinessException("نام خانوادگی اجباری است.");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        public static PersonName Create(string firstName, string lastName)
            => new PersonName(firstName, lastName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName.ToLowerInvariant();
            yield return LastName.ToLowerInvariant();
        }

        public override string ToString() => $"{FirstName} {LastName}";

        public static implicit operator string(PersonName name)
            => name.ToString();
    }

}
