using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingPlanCreditGrantResetType.BillingPlanCreditGrantResetTypeSerializer))]
[Serializable]
public readonly record struct BillingPlanCreditGrantResetType : IStringEnum
{
    public static readonly BillingPlanCreditGrantResetType NoReset = new(Values.NoReset);

    public static readonly BillingPlanCreditGrantResetType PlanPeriod = new(Values.PlanPeriod);

    public BillingPlanCreditGrantResetType(string value)
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
    public static BillingPlanCreditGrantResetType FromCustom(string value)
    {
        return new BillingPlanCreditGrantResetType(value);
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

    public static bool operator ==(BillingPlanCreditGrantResetType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingPlanCreditGrantResetType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingPlanCreditGrantResetType value) => value.Value;

    public static explicit operator BillingPlanCreditGrantResetType(string value) => new(value);

    internal class BillingPlanCreditGrantResetTypeSerializer
        : JsonConverter<BillingPlanCreditGrantResetType>
    {
        public override BillingPlanCreditGrantResetType Read(
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
            return new BillingPlanCreditGrantResetType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingPlanCreditGrantResetType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingPlanCreditGrantResetType ReadAsPropertyName(
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
            return new BillingPlanCreditGrantResetType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingPlanCreditGrantResetType value,
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
        public const string NoReset = "no_reset";

        public const string PlanPeriod = "plan_period";
    }
}
