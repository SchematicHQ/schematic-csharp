using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RulesengineRuleType.RulesengineRuleTypeSerializer))]
[Serializable]
public readonly record struct RulesengineRuleType : IStringEnum
{
    public static readonly RulesengineRuleType CompanyOverride = new(Values.CompanyOverride);

    public static readonly RulesengineRuleType CompanyOverrideUsageExceeded = new(
        Values.CompanyOverrideUsageExceeded
    );

    public static readonly RulesengineRuleType Default = new(Values.Default);

    public static readonly RulesengineRuleType GlobalOverride = new(Values.GlobalOverride);

    public static readonly RulesengineRuleType PlanEntitlement = new(Values.PlanEntitlement);

    public static readonly RulesengineRuleType PlanEntitlementUsageExceeded = new(
        Values.PlanEntitlementUsageExceeded
    );

    public static readonly RulesengineRuleType Standard = new(Values.Standard);

    public RulesengineRuleType(string value)
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
    public static RulesengineRuleType FromCustom(string value)
    {
        return new RulesengineRuleType(value);
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

    public static bool operator ==(RulesengineRuleType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineRuleType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineRuleType value) => value.Value;

    public static explicit operator RulesengineRuleType(string value) => new(value);

    internal class RulesengineRuleTypeSerializer : JsonConverter<RulesengineRuleType>
    {
        public override RulesengineRuleType Read(
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
            return new RulesengineRuleType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineRuleType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineRuleType ReadAsPropertyName(
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
            return new RulesengineRuleType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineRuleType value,
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
