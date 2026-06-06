namespace ScrapDealer.Domain.ValueObjects.Base;

public class Amount : ValueObject
{
    public decimal Value { get; }

    private Amount() { }
    private Amount(decimal value)
    {
        Value = value;
    }

    public static Amount Create(decimal value)
    {
        return new Amount(value);
    }

    public override bool Equals(object obj)
    {
        if (obj is Amount other)
            return Value == other.Value;

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Amount amount)
        => amount.Value;

    public static implicit operator Amount(decimal value)
        => Create(value);
}
