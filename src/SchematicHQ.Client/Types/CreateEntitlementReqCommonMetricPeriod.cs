using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CreateEntitlementReqCommonMetricPeriod.CreateEntitlementReqCommonMetricPeriodSerializer)
)]
[Serializable]
public readonly record struct CreateEntitlementReqCommonMetricPeriod : IStringEnum
{
    public static readonly CreateEntitlementReqCommonMetricPeriod AllTime = new(Values.AllTime);

    public static readonly CreateEntitlementReqCommonMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly CreateEntitlementReqCommonMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public static readonly CreateEntitlementReqCommonMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public CreateEntitlementReqCommonMetricPeriod(string value)
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
    public static CreateEntitlementReqCommonMetricPeriod FromCustom(string value)
    {
        return new CreateEntitlementReqCommonMetricPeriod(value);
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

    public static bool operator ==(CreateEntitlementReqCommonMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateEntitlementReqCommonMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateEntitlementReqCommonMetricPeriod value) =>
        value.Value;

    public static explicit operator CreateEntitlementReqCommonMetricPeriod(string value) =>
        new(value);

    internal class CreateEntitlementReqCommonMetricPeriodSerializer
        : JsonConverter<CreateEntitlementReqCommonMetricPeriod>
    {
        public override CreateEntitlementReqCommonMetricPeriod Read(
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
            return new CreateEntitlementReqCommonMetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateEntitlementReqCommonMetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreateEntitlementReqCommonMetricPeriod ReadAsPropertyName(
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
            return new CreateEntitlementReqCommonMetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateEntitlementReqCommonMetricPeriod value,
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
