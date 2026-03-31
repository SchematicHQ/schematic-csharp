using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(UpdateCompanyOverrideRequestBodyMetricPeriod.UpdateCompanyOverrideRequestBodyMetricPeriodSerializer)
)]
[Serializable]
public readonly record struct UpdateCompanyOverrideRequestBodyMetricPeriod : IStringEnum
{
    public static readonly UpdateCompanyOverrideRequestBodyMetricPeriod AllTime = new(
        Values.AllTime
    );

    public static readonly UpdateCompanyOverrideRequestBodyMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly UpdateCompanyOverrideRequestBodyMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public static readonly UpdateCompanyOverrideRequestBodyMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public UpdateCompanyOverrideRequestBodyMetricPeriod(string value)
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
    public static UpdateCompanyOverrideRequestBodyMetricPeriod FromCustom(string value)
    {
        return new UpdateCompanyOverrideRequestBodyMetricPeriod(value);
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
        UpdateCompanyOverrideRequestBodyMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateCompanyOverrideRequestBodyMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateCompanyOverrideRequestBodyMetricPeriod value) =>
        value.Value;

    public static explicit operator UpdateCompanyOverrideRequestBodyMetricPeriod(string value) =>
        new(value);

    internal class UpdateCompanyOverrideRequestBodyMetricPeriodSerializer
        : JsonConverter<UpdateCompanyOverrideRequestBodyMetricPeriod>
    {
        public override UpdateCompanyOverrideRequestBodyMetricPeriod Read(
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
            return new UpdateCompanyOverrideRequestBodyMetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            UpdateCompanyOverrideRequestBodyMetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override UpdateCompanyOverrideRequestBodyMetricPeriod ReadAsPropertyName(
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
            return new UpdateCompanyOverrideRequestBodyMetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            UpdateCompanyOverrideRequestBodyMetricPeriod value,
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

        public const string CurrentMonth = "current_month";

        public const string CurrentWeek = "current_week";

        public const string CurrentDay = "current_day";
    }
}
