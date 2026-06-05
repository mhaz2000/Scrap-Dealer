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
