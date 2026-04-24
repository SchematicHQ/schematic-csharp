using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(RulesengineMetricPeriodMonthReset.RulesengineMetricPeriodMonthResetSerializer)
)]
[Serializable]
public readonly record struct RulesengineMetricPeriodMonthReset : IStringEnum
{
    public static readonly RulesengineMetricPeriodMonthReset BillingCycle = new(
        Values.BillingCycle
    );

    public static readonly RulesengineMetricPeriodMonthReset FirstOfMonth = new(
        Values.FirstOfMonth
    );

    public RulesengineMetricPeriodMonthReset(string value)
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
    public static RulesengineMetricPeriodMonthReset FromCustom(string value)
    {
        return new RulesengineMetricPeriodMonthReset(value);
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

    public static bool operator ==(RulesengineMetricPeriodMonthReset value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineMetricPeriodMonthReset value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineMetricPeriodMonthReset value) => value.Value;

    public static explicit operator RulesengineMetricPeriodMonthReset(string value) => new(value);

    internal class RulesengineMetricPeriodMonthResetSerializer
        : JsonConverter<RulesengineMetricPeriodMonthReset>
    {
        public override RulesengineMetricPeriodMonthReset Read(
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
            return new RulesengineMetricPeriodMonthReset(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineMetricPeriodMonthReset value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineMetricPeriodMonthReset ReadAsPropertyName(
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
            return new RulesengineMetricPeriodMonthReset(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineMetricPeriodMonthReset value,
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
        public const string BillingCycle = "billing_cycle";

        public const string FirstOfMonth = "first_of_month";
    }
}
