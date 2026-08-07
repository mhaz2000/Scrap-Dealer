using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Base;

public class Weight : ValueObject
{
    public double Value { get; }

    private Weight() { }
    private Weight(double value)
    {
        Value = value;
    }

    public static Weight Create(double value)
    {
        if (value <= 0)
            throw new BusinessException("وزن نمی‌تواند 0 یا کمتر باشد.");

        return new Weight(value);
    }

    public override bool Equals(object obj)
    {
        if (obj is Weight other)
            return Value == other.Value;

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator double(Weight amount)
        => amount.Value;

    public static implicit operator Weight(double value)
        => Create(value);
}
