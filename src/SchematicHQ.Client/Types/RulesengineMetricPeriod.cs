using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RulesengineMetricPeriod.RulesengineMetricPeriodSerializer))]
[Serializable]
public readonly record struct RulesengineMetricPeriod : IStringEnum
{
    public static readonly RulesengineMetricPeriod AllTime = new(Values.AllTime);

    public static readonly RulesengineMetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly RulesengineMetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly RulesengineMetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public RulesengineMetricPeriod(string value)
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
    public static RulesengineMetricPeriod FromCustom(string value)
    {
        return new RulesengineMetricPeriod(value);
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

    public static bool operator ==(RulesengineMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineMetricPeriod value) => value.Value;

    public static explicit operator RulesengineMetricPeriod(string value) => new(value);

    internal class RulesengineMetricPeriodSerializer : JsonConverter<RulesengineMetricPeriod>
    {
        public override RulesengineMetricPeriod Read(
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
            return new RulesengineMetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineMetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineMetricPeriod ReadAsPropertyName(
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
            return new RulesengineMetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineMetricPeriod value,
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
        public const string AllTime = "all_time";

        public const string CurrentDay = "current_day";

        public const string CurrentMonth = "current_month";

        public const string CurrentWeek = "current_week";
    }
}
