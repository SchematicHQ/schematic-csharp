using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(RulesengineFeatureEntitlementMonthReset.RulesengineFeatureEntitlementMonthResetSerializer)
)]
[Serializable]
public readonly record struct RulesengineFeatureEntitlementMonthReset : IStringEnum
{
    public static readonly RulesengineFeatureEntitlementMonthReset FirstOfMonth = new(
        Values.FirstOfMonth
    );

    public static readonly RulesengineFeatureEntitlementMonthReset BillingCycle = new(
        Values.BillingCycle
    );

    public RulesengineFeatureEntitlementMonthReset(string value)
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
    public static RulesengineFeatureEntitlementMonthReset FromCustom(string value)
    {
        return new RulesengineFeatureEntitlementMonthReset(value);
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

    public static bool operator ==(RulesengineFeatureEntitlementMonthReset value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineFeatureEntitlementMonthReset value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineFeatureEntitlementMonthReset value) =>
        value.Value;

    public static explicit operator RulesengineFeatureEntitlementMonthReset(string value) =>
        new(value);

    internal class RulesengineFeatureEntitlementMonthResetSerializer
        : JsonConverter<RulesengineFeatureEntitlementMonthReset>
    {
        public override RulesengineFeatureEntitlementMonthReset Read(
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
            return new RulesengineFeatureEntitlementMonthReset(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineFeatureEntitlementMonthReset value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineFeatureEntitlementMonthReset ReadAsPropertyName(
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
            return new RulesengineFeatureEntitlementMonthReset(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineFeatureEntitlementMonthReset value,
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
        public const string FirstOfMonth = "first_of_month";

        public const string BillingCycle = "billing_cycle";
    }
}
