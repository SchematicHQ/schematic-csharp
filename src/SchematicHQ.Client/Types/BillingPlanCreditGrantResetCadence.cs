using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(BillingPlanCreditGrantResetCadence.BillingPlanCreditGrantResetCadenceSerializer)
)]
[Serializable]
public readonly record struct BillingPlanCreditGrantResetCadence : IStringEnum
{
    public static readonly BillingPlanCreditGrantResetCadence Daily = new(Values.Daily);

    public static readonly BillingPlanCreditGrantResetCadence Monthly = new(Values.Monthly);

    public static readonly BillingPlanCreditGrantResetCadence Weekly = new(Values.Weekly);

    public static readonly BillingPlanCreditGrantResetCadence Yearly = new(Values.Yearly);

    public BillingPlanCreditGrantResetCadence(string value)
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
    public static BillingPlanCreditGrantResetCadence FromCustom(string value)
    {
        return new BillingPlanCreditGrantResetCadence(value);
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

    public static bool operator ==(BillingPlanCreditGrantResetCadence value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingPlanCreditGrantResetCadence value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingPlanCreditGrantResetCadence value) => value.Value;

    public static explicit operator BillingPlanCreditGrantResetCadence(string value) => new(value);

    internal class BillingPlanCreditGrantResetCadenceSerializer
        : JsonConverter<BillingPlanCreditGrantResetCadence>
    {
        public override BillingPlanCreditGrantResetCadence Read(
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
            return new BillingPlanCreditGrantResetCadence(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingPlanCreditGrantResetCadence value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingPlanCreditGrantResetCadence ReadAsPropertyName(
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
            return new BillingPlanCreditGrantResetCadence(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingPlanCreditGrantResetCadence value,
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
        public const string Daily = "daily";

        public const string Monthly = "monthly";

        public const string Weekly = "weekly";

        public const string Yearly = "yearly";
    }
}
