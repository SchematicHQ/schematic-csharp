using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<TraitType>))]
[Serializable]
public readonly record struct TraitType : IStringEnum
{
    public static readonly TraitType Boolean = new(Values.Boolean);

    public static readonly TraitType Currency = new(Values.Currency);

    public static readonly TraitType Date = new(Values.Date);

    public static readonly TraitType Number = new(Values.Number);

    public static readonly TraitType String = new(Values.String);

    public static readonly TraitType Url = new(Values.Url);

    public TraitType(string value)
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
    public static TraitType FromCustom(string value)
    {
        return new TraitType(value);
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

    public static bool operator ==(TraitType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(TraitType value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(TraitType value) => value.Value;

    public static explicit operator TraitType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Currency = "currency";

        public const string Date = "date";

        public const string Number = "number";

        public const string String = "string";

        public const string Url = "url";
    }
}
