using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureCompanyUserResponseDataAllocationType>))]
[Serializable]
public readonly record struct FeatureCompanyUserResponseDataAllocationType : IStringEnum
{
    public static readonly FeatureCompanyUserResponseDataAllocationType Boolean = new(
        Values.Boolean
    );

    public static readonly FeatureCompanyUserResponseDataAllocationType Numeric = new(
        Values.Numeric
    );

    public static readonly FeatureCompanyUserResponseDataAllocationType Trait = new(Values.Trait);

    public static readonly FeatureCompanyUserResponseDataAllocationType Unlimited = new(
        Values.Unlimited
    );

    public FeatureCompanyUserResponseDataAllocationType(string value)
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
    public static FeatureCompanyUserResponseDataAllocationType FromCustom(string value)
    {
        return new FeatureCompanyUserResponseDataAllocationType(value);
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
        FeatureCompanyUserResponseDataAllocationType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        FeatureCompanyUserResponseDataAllocationType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(FeatureCompanyUserResponseDataAllocationType value) =>
        value.Value;

    public static explicit operator FeatureCompanyUserResponseDataAllocationType(string value) =>
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
