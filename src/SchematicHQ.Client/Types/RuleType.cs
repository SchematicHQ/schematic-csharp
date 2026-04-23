using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RuleType.RuleTypeSerializer))]
[Serializable]
public readonly record struct RuleType : IStringEnum
{
    public static readonly RuleType CompanyOverride = new(Values.CompanyOverride);

    public static readonly RuleType CompanyOverrideUsageExceeded = new(
        Values.CompanyOverrideUsageExceeded
    );

    public static readonly RuleType Default = new(Values.Default);

    public static readonly RuleType GlobalOverride = new(Values.GlobalOverride);

    public static readonly RuleType PlanEntitlement = new(Values.PlanEntitlement);

    public static readonly RuleType PlanEntitlementUsageExceeded = new(
        Values.PlanEntitlementUsageExceeded
    );

    public static readonly RuleType Standard = new(Values.Standard);

    public RuleType(string value)
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
    public static RuleType FromCustom(string value)
    {
        return new RuleType(value);
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

    public static bool operator ==(RuleType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(RuleType value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(RuleType value) => value.Value;

    public static explicit operator RuleType(string value) => new(value);

    internal class RuleTypeSerializer : JsonConverter<RuleType>
    {
        public override RuleType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new RuleType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RuleType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RuleType ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new RuleType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RuleType value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CompanyOverride = "company_override";

        public const string CompanyOverrideUsageExceeded = "company_override_usage_exceeded";

        public const string Default = "default";

        public const string GlobalOverride = "global_override";

        public const string PlanEntitlement = "plan_entitlement";

        public const string PlanEntitlementUsageExceeded = "plan_entitlement_usage_exceeded";

        public const string Standard = "standard";
    }
}
