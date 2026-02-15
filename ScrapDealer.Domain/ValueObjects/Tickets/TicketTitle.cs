using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.Tickets
{
    public class TicketTitle : ValueObject
    {
        public string Value { get; }

        private TicketTitle(string value)
        {
            Value = value.Trim();
        }

        public static TicketTitle Create(string value)
        {
            return new TicketTitle(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            if (obj is TicketTitle other)
                return Value == other.Value;

            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(TicketTitle ticketTitle)
            => ticketTitle.Value;

        public static implicit operator TicketTitle(string value)
            => Create(value);
    }
}
