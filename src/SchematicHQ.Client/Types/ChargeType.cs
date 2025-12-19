using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ChargeType>))]
[Serializable]
public readonly record struct ChargeType : IStringEnum
{
    public static readonly ChargeType Free = new(Values.Free);

    public static readonly ChargeType OneTime = new(Values.OneTime);

    public static readonly ChargeType Recurring = new(Values.Recurring);

    public ChargeType(string value)
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
    public static ChargeType FromCustom(string value)
    {
        return new ChargeType(value);
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

    public static bool operator ==(ChargeType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(ChargeType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ChargeType value) => value.Value;

    public static explicit operator ChargeType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Free = "free";

        public const string OneTime = "one_time";

        public const string Recurring = "recurring";
    }
}
