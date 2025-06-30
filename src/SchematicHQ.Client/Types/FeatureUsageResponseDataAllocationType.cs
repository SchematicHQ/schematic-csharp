using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureUsageResponseDataAllocationType>))]
[Serializable]
public readonly record struct FeatureUsageResponseDataAllocationType : IStringEnum
{
    public static readonly FeatureUsageResponseDataAllocationType Boolean = new(Values.Boolean);

    public static readonly FeatureUsageResponseDataAllocationType Numeric = new(Values.Numeric);

    public static readonly FeatureUsageResponseDataAllocationType Trait = new(Values.Trait);

    public static readonly FeatureUsageResponseDataAllocationType Unlimited = new(Values.Unlimited);

    public FeatureUsageResponseDataAllocationType(string value)
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
    public static FeatureUsageResponseDataAllocationType FromCustom(string value)
    {
        return new FeatureUsageResponseDataAllocationType(value);
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

    public static bool operator ==(FeatureUsageResponseDataAllocationType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FeatureUsageResponseDataAllocationType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FeatureUsageResponseDataAllocationType value) =>
        value.Value;

    public static explicit operator FeatureUsageResponseDataAllocationType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Numeric = "numeric";

        public const string Trait = "trait";

        public const string Unlimited = "unlimited";
    }
}
