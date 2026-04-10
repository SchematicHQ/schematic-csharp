using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod.CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriodSerializer)
)]
[Serializable]
public readonly record struct CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod
    : IStringEnum
{
    public static readonly CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod AllTime = new(
        Values.AllTime
    );

    public static readonly CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod CurrentMonth =
        new(Values.CurrentMonth);

    public static readonly CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod CurrentWeek =
        new(Values.CurrentWeek);

    public static readonly CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod CurrentDay =
        new(Values.CurrentDay);

    public CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod(string value)
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
    public static CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod FromCustom(string value)
    {
        return new CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod(value);
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
        CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod value
    ) => value.Value;

    public static explicit operator CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod(
        string value
    ) => new(value);

    internal class CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriodSerializer
        : JsonConverter<CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod>
    {
        public override CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod Read(
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
            return new CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod ReadAsPropertyName(
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
            return new CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateBillingLinkedPlanEntitlementRequestBodyMetricPeriod value,
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
