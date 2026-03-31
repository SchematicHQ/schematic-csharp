using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CreateEntitlementReqCommonMetricPeriodMonthReset.CreateEntitlementReqCommonMetricPeriodMonthResetSerializer)
)]
[Serializable]
public readonly record struct CreateEntitlementReqCommonMetricPeriodMonthReset : IStringEnum
{
    public static readonly CreateEntitlementReqCommonMetricPeriodMonthReset FirstOfMonth = new(
        Values.FirstOfMonth
    );

    public static readonly CreateEntitlementReqCommonMetricPeriodMonthReset BillingCycle = new(
        Values.BillingCycle
    );

    public CreateEntitlementReqCommonMetricPeriodMonthReset(string value)
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
    public static CreateEntitlementReqCommonMetricPeriodMonthReset FromCustom(string value)
    {
        return new CreateEntitlementReqCommonMetricPeriodMonthReset(value);
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
        CreateEntitlementReqCommonMetricPeriodMonthReset value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEntitlementReqCommonMetricPeriodMonthReset value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateEntitlementReqCommonMetricPeriodMonthReset value
    ) => value.Value;

    public static explicit operator CreateEntitlementReqCommonMetricPeriodMonthReset(
        string value
    ) => new(value);

    internal class CreateEntitlementReqCommonMetricPeriodMonthResetSerializer
        : JsonConverter<CreateEntitlementReqCommonMetricPeriodMonthReset>
    {
        public override CreateEntitlementReqCommonMetricPeriodMonthReset Read(
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
            return new CreateEntitlementReqCommonMetricPeriodMonthReset(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateEntitlementReqCommonMetricPeriodMonthReset value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreateEntitlementReqCommonMetricPeriodMonthReset ReadAsPropertyName(
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
            return new CreateEntitlementReqCommonMetricPeriodMonthReset(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateEntitlementReqCommonMetricPeriodMonthReset value,
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
