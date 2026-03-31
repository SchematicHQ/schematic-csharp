using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(UpdatePlanEntitlementRequestBodyMetricPeriod.UpdatePlanEntitlementRequestBodyMetricPeriodSerializer)
)]
[Serializable]
public readonly record struct UpdatePlanEntitlementRequestBodyMetricPeriod : IStringEnum
{
    public static readonly UpdatePlanEntitlementRequestBodyMetricPeriod AllTime = new(
        Values.AllTime
    );

    public static readonly UpdatePlanEntitlementRequestBodyMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly UpdatePlanEntitlementRequestBodyMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public static readonly UpdatePlanEntitlementRequestBodyMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public UpdatePlanEntitlementRequestBodyMetricPeriod(string value)
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
    public static UpdatePlanEntitlementRequestBodyMetricPeriod FromCustom(string value)
    {
        return new UpdatePlanEntitlementRequestBodyMetricPeriod(value);
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
        UpdatePlanEntitlementRequestBodyMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdatePlanEntitlementRequestBodyMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdatePlanEntitlementRequestBodyMetricPeriod value) =>
        value.Value;

    public static explicit operator UpdatePlanEntitlementRequestBodyMetricPeriod(string value) =>
        new(value);

    internal class UpdatePlanEntitlementRequestBodyMetricPeriodSerializer
        : JsonConverter<UpdatePlanEntitlementRequestBodyMetricPeriod>
    {
        public override UpdatePlanEntitlementRequestBodyMetricPeriod Read(
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
            return new UpdatePlanEntitlementRequestBodyMetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            UpdatePlanEntitlementRequestBodyMetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override UpdatePlanEntitlementRequestBodyMetricPeriod ReadAsPropertyName(
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
            return new UpdatePlanEntitlementRequestBodyMetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            UpdatePlanEntitlementRequestBodyMetricPeriod value,
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
