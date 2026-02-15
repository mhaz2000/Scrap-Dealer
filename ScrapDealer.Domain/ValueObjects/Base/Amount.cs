using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Base;

public class Title : ValueObject
{
    public string Value { get; }

    private Title(string value)
    {
        Value = value.Trim();
    }

    public static Title Create(string value)
    {
        return new Title(value);
    }

    public override string ToString() => Value;

    public override bool Equals(object obj)
    {
        if (obj is Title other)
            return Value == other.Value;

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Title ticketTitle)
        => ticketTitle.Value;

    public static implicit operator Title(string value)
        => Create(value);
}

public class Amount : ValueObject
{
    public decimal Value { get; }

    private Amount(decimal value)
    {
        if (value < 0)
            throw new BusinessException("مبلغ باید بزرکتر از صفر باشد.");

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