using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureCompanyResponseDataAllocationType>))]
[Serializable]
public readonly record struct FeatureCompanyResponseDataAllocationType : IStringEnum
{
    public static readonly FeatureCompanyResponseDataAllocationType Boolean = new(Values.Boolean);

    public static readonly FeatureCompanyResponseDataAllocationType Numeric = new(Values.Numeric);

    public static readonly FeatureCompanyResponseDataAllocationType Trait = new(Values.Trait);

    public static readonly FeatureCompanyResponseDataAllocationType Unlimited = new(
        Values.Unlimited
    );

    public FeatureCompanyResponseDataAllocationType(string value)
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
    public static FeatureCompanyResponseDataAllocationType FromCustom(string value)
    {
        return new FeatureCompanyResponseDataAllocationType(value);
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

    public static bool operator ==(
        FeatureCompanyResponseDataAllocationType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        FeatureCompanyResponseDataAllocationType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(FeatureCompanyResponseDataAllocationType value) =>
        value.Value;

    public static explicit operator FeatureCompanyResponseDataAllocationType(string value) =>
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
