using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.SaleOrders
{
    public class Location : ValueObject
    {
        public double Latitude { get; }
        public double Longitude { get; }

        private Location() { }
        private Location(double latitude, double longitude)
        {
            if (!IsValidCoordinate(latitude, longitude))
                throw new BusinessException("موقعیت جغرافیایی معتبر نیست یا خارج از محدوده ایران است.");

            Latitude = latitude;
            Longitude = longitude;
        }

        public static Location Create(double latitude, double longitude)
            => new(latitude, longitude);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }

        public static bool IsValidCoordinate(double latitude, double longitude)
        {
            // Must be valid numeric ranges
            if (latitude < -90 || latitude > 90)
                return false;

            if (longitude < -180 || longitude > 180)
                return false;

            // Iran bounding box:
            // Latitude:   25   to 40
            // Longitude:  44   to 63.5
            bool insideIran =
                latitude >= 25.0 && latitude <= 40.0 &&
                longitude >= 44.0 && longitude <= 63.5;

            return insideIran;
        }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }

}
