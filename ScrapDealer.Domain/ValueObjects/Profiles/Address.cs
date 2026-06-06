using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Profiles
{
    public class ProfileAddress : ValueObject
    {
        public string Province { get; }
        public string City { get; }
        public string? PostalCode { get; }
        public string Description { get; }
        public ActivityArea? ActivityArea { get; set; }

        private ProfileAddress() { }
        private ProfileAddress(string province, string city, string? postalCode, string description, ActivityArea? activityArea)
        {
            if (string.IsNullOrWhiteSpace(province))
                throw new BusinessException("استان اجباری است.");

            if (string.IsNullOrWhiteSpace(city))
                throw new BusinessException("شهر اجباری است.");

            if (!string.IsNullOrEmpty(postalCode) && !IsValidPostalCode(postalCode))
                throw new BusinessException("کد پستی معتبر نیست.");

            if (string.IsNullOrWhiteSpace(description))
                throw new BusinessException("آدرس کامل اجباری است.");

            Province = province.Trim();
            City = city.Trim();
            PostalCode = postalCode?.Trim();
            Description = description.Trim();
            ActivityArea = activityArea;
        }

        public static ProfileAddress Create(string province, string city, string? postalCode, string description, ActivityArea? activityArea)
            => new ProfileAddress(province, city, postalCode, description, activityArea);

        private bool IsValidPostalCode(string postalCode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(postalCode, @"^\d{10}$");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province.ToLower();
            yield return City.ToLower();
            yield return PostalCode;
            yield return Description.ToLower();
            yield return ActivityArea.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Province}، {City}، کد پستی: {PostalCode}، آدرس: {Description}، محل فعالیت: {ActivityArea}";
        }
    }

}
