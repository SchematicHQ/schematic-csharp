using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RulesengineCompanyMetricPeriod.RulesengineCompanyMetricPeriodSerializer))]
[Serializable]
public readonly record struct RulesengineCompanyMetricPeriod : IStringEnum
{
    public static readonly RulesengineCompanyMetricPeriod AllTime = new(Values.AllTime);

    public static readonly RulesengineCompanyMetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly RulesengineCompanyMetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly RulesengineCompanyMetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public RulesengineCompanyMetricPeriod(string value)
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
    public static RulesengineCompanyMetricPeriod FromCustom(string value)
    {
        return new RulesengineCompanyMetricPeriod(value);
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

    public static bool operator ==(RulesengineCompanyMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineCompanyMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineCompanyMetricPeriod value) => value.Value;

    public static explicit operator RulesengineCompanyMetricPeriod(string value) => new(value);

    internal class RulesengineCompanyMetricPeriodSerializer
        : JsonConverter<RulesengineCompanyMetricPeriod>
    {
        public override RulesengineCompanyMetricPeriod Read(
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
            return new RulesengineCompanyMetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineCompanyMetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineCompanyMetricPeriod ReadAsPropertyName(
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
            return new RulesengineCompanyMetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineCompanyMetricPeriod value,
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
