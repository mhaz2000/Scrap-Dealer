using ScrapDealer.Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapDealer.Domain.ValueObjects.Notifications
{
    public class NotificationContent : ValueObject
    {
        public string Value { get; }

        private NotificationContent(string value)
        {
            Value = value.Trim();
        }

        public static NotificationContent Create(string value)
        {
            return new NotificationContent(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            if (obj is NotificationContent other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(NotificationContent NotificationContent)
            => NotificationContent.Value;

        public static implicit operator NotificationContent(string value)
            => Create(value);
    }
}
