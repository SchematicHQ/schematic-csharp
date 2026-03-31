using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(BillingCreditGrantZeroedOutReason.BillingCreditGrantZeroedOutReasonSerializer)
)]
[Serializable]
public readonly record struct BillingCreditGrantZeroedOutReason : IStringEnum
{
    public static readonly BillingCreditGrantZeroedOutReason Expired = new(Values.Expired);

    public static readonly BillingCreditGrantZeroedOutReason Manual = new(Values.Manual);

    public static readonly BillingCreditGrantZeroedOutReason PlanChange = new(Values.PlanChange);

    public static readonly BillingCreditGrantZeroedOutReason PlanPeriodReset = new(
        Values.PlanPeriodReset
    );

    public BillingCreditGrantZeroedOutReason(string value)
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
    public static BillingCreditGrantZeroedOutReason FromCustom(string value)
    {
        return new BillingCreditGrantZeroedOutReason(value);
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

    public static bool operator ==(BillingCreditGrantZeroedOutReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditGrantZeroedOutReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditGrantZeroedOutReason value) => value.Value;

    public static explicit operator BillingCreditGrantZeroedOutReason(string value) => new(value);

    internal class BillingCreditGrantZeroedOutReasonSerializer
        : JsonConverter<BillingCreditGrantZeroedOutReason>
    {
        public override BillingCreditGrantZeroedOutReason Read(
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
            return new BillingCreditGrantZeroedOutReason(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditGrantZeroedOutReason value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditGrantZeroedOutReason ReadAsPropertyName(
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
            return new BillingCreditGrantZeroedOutReason(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditGrantZeroedOutReason value,
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
        public const string Expired = "expired";

        public const string Manual = "manual";

        public const string PlanChange = "plan_change";

        public const string PlanPeriodReset = "plan_period_reset";
    }
}
