using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineEntitlementValueType>))]
[Serializable]
public readonly record struct RulesengineEntitlementValueType : IStringEnum
{
    public static readonly RulesengineEntitlementValueType Boolean = new(Values.Boolean);

    public static readonly RulesengineEntitlementValueType Credit = new(Values.Credit);

    public static readonly RulesengineEntitlementValueType Numeric = new(Values.Numeric);

    public static readonly RulesengineEntitlementValueType Trait = new(Values.Trait);

    public static readonly RulesengineEntitlementValueType Unknown = new(Values.Unknown);

    public static readonly RulesengineEntitlementValueType Unlimited = new(Values.Unlimited);

    public RulesengineEntitlementValueType(string value)
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
    public static RulesengineEntitlementValueType FromCustom(string value)
    {
        return new RulesengineEntitlementValueType(value);
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

    public static bool operator ==(RulesengineEntitlementValueType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineEntitlementValueType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineEntitlementValueType value) => value.Value;

    public static explicit operator RulesengineEntitlementValueType(string value) => new(value);

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

        public const string Unknown = "unknown";

        public const string Unlimited = "unlimited";
    }
}
