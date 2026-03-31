using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingCreditExpiryUnit.BillingCreditExpiryUnitSerializer))]
[Serializable]
public readonly record struct BillingCreditExpiryUnit : IStringEnum
{
    public static readonly BillingCreditExpiryUnit BillingPeriods = new(Values.BillingPeriods);

    public static readonly BillingCreditExpiryUnit Days = new(Values.Days);

    public BillingCreditExpiryUnit(string value)
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
    public static BillingCreditExpiryUnit FromCustom(string value)
    {
        return new BillingCreditExpiryUnit(value);
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

    public static bool operator ==(BillingCreditExpiryUnit value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditExpiryUnit value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditExpiryUnit value) => value.Value;

    public static explicit operator BillingCreditExpiryUnit(string value) => new(value);

    internal class BillingCreditExpiryUnitSerializer : JsonConverter<BillingCreditExpiryUnit>
    {
        public override BillingCreditExpiryUnit Read(
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
            return new BillingCreditExpiryUnit(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditExpiryUnit value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditExpiryUnit ReadAsPropertyName(
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
            return new BillingCreditExpiryUnit(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditExpiryUnit value,
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
        public const string BillingPeriods = "billing_periods";

        public const string Days = "days";
    }
}
