using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureType>))]
[Serializable]
public readonly record struct FeatureType : IStringEnum
{
    public static readonly FeatureType Boolean = new(Values.Boolean);

    public static readonly FeatureType Event = new(Values.Event);

    public static readonly FeatureType Trait = new(Values.Trait);

    public FeatureType(string value)
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
    public static FeatureType FromCustom(string value)
    {
        return new FeatureType(value);
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

    public static bool operator ==(FeatureType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FeatureType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FeatureType value) => value.Value;

    public static explicit operator FeatureType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Event = "event";

        public const string Trait = "trait";
    }
}
