using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(MetricPeriod.MetricPeriodSerializer))]
[Serializable]
public readonly record struct MetricPeriod : IStringEnum
{
    public static readonly MetricPeriod AllTime = new(Values.AllTime);

    public static readonly MetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly MetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly MetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public MetricPeriod(string value)
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
    public static MetricPeriod FromCustom(string value)
    {
        return new MetricPeriod(value);
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

    public static bool operator ==(MetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MetricPeriod value) => value.Value;

    public static explicit operator MetricPeriod(string value) => new(value);

    internal class MetricPeriodSerializer : JsonConverter<MetricPeriod>
    {
        public override MetricPeriod Read(
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
            return new MetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MetricPeriod ReadAsPropertyName(
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
            return new MetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MetricPeriod value,
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
