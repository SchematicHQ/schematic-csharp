using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateCompanyOverrideRequestBodyValueType>))]
[Serializable]
public readonly record struct UpdateCompanyOverrideRequestBodyValueType : IStringEnum
{
    public static readonly UpdateCompanyOverrideRequestBodyValueType Boolean = new(Values.Boolean);

    public static readonly UpdateCompanyOverrideRequestBodyValueType Credit = new(Values.Credit);

    public static readonly UpdateCompanyOverrideRequestBodyValueType Numeric = new(Values.Numeric);

    public static readonly UpdateCompanyOverrideRequestBodyValueType Trait = new(Values.Trait);

    public static readonly UpdateCompanyOverrideRequestBodyValueType Unlimited = new(
        Values.Unlimited
    );

    public UpdateCompanyOverrideRequestBodyValueType(string value)
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
    public static UpdateCompanyOverrideRequestBodyValueType FromCustom(string value)
    {
        return new UpdateCompanyOverrideRequestBodyValueType(value);
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
        UpdateCompanyOverrideRequestBodyValueType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateCompanyOverrideRequestBodyValueType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateCompanyOverrideRequestBodyValueType value) =>
        value.Value;

    public static explicit operator UpdateCompanyOverrideRequestBodyValueType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Credit = "credit";

        public const string Numeric = "numeric";

        public const string Trait = "trait";

        public const string Unlimited = "unlimited";
    }
}
