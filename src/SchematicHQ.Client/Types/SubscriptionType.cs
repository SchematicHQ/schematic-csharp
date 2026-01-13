using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<SubscriptionType>))]
[Serializable]
public readonly record struct SubscriptionType : IStringEnum
{
    public static readonly SubscriptionType Free = new(Values.Free);

    public static readonly SubscriptionType OneTime = new(Values.OneTime);

    public static readonly SubscriptionType Paid = new(Values.Paid);

    public static readonly SubscriptionType Trial = new(Values.Trial);

    public SubscriptionType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static SubscriptionType FromCustom(string value)
    {
        return new SubscriptionType(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(SubscriptionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionType value) => value.Value;

    public static explicit operator SubscriptionType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Free = "free";

        public const string OneTime = "one_time";

        public const string Paid = "paid";

        public const string Trial = "trial";
    }
}
