using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Domain.ValueObjects.Tickets;
public class MessageContent : ValueObject
{
    public string Value { get; }

    private MessageContent() { }
    private MessageContent(string value)
    {
        Value = value.Trim();
    }

    public static MessageContent Create(string value)
    {
        return new MessageContent(value);
    }

    public override string ToString() => Value;

    public override bool Equals(object obj)
    {
        if (obj is MessageContent other)
            return Value == other.Value;

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(MessageContent messageContent)
        => messageContent.Value;

    public static implicit operator MessageContent(string value)
        => Create(value);
}
