using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdatePlanEntitlementRequestBodyValueType>))]
[Serializable]
public readonly record struct UpdatePlanEntitlementRequestBodyValueType : IStringEnum
{
    public static readonly UpdatePlanEntitlementRequestBodyValueType Boolean = new(Values.Boolean);

    public static readonly UpdatePlanEntitlementRequestBodyValueType Credit = new(Values.Credit);

    public static readonly UpdatePlanEntitlementRequestBodyValueType Numeric = new(Values.Numeric);

    public static readonly UpdatePlanEntitlementRequestBodyValueType Trait = new(Values.Trait);

    public static readonly UpdatePlanEntitlementRequestBodyValueType Unlimited = new(
        Values.Unlimited
    );

    public UpdatePlanEntitlementRequestBodyValueType(string value)
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
    public static UpdatePlanEntitlementRequestBodyValueType FromCustom(string value)
    {
        return new UpdatePlanEntitlementRequestBodyValueType(value);
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
        UpdatePlanEntitlementRequestBodyValueType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdatePlanEntitlementRequestBodyValueType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdatePlanEntitlementRequestBodyValueType value) =>
        value.Value;

    public static explicit operator UpdatePlanEntitlementRequestBodyValueType(string value) =>
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
