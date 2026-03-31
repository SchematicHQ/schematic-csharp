using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingCreditExpiryType.BillingCreditExpiryTypeSerializer))]
[Serializable]
public readonly record struct BillingCreditExpiryType : IStringEnum
{
    public static readonly BillingCreditExpiryType Duration = new(Values.Duration);

    public static readonly BillingCreditExpiryType EndOfBillingPeriod = new(
        Values.EndOfBillingPeriod
    );

    public static readonly BillingCreditExpiryType EndOfNextBillingPeriod = new(
        Values.EndOfNextBillingPeriod
    );

    public static readonly BillingCreditExpiryType EndOfTrial = new(Values.EndOfTrial);

    public static readonly BillingCreditExpiryType NoExpiry = new(Values.NoExpiry);

    public BillingCreditExpiryType(string value)
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
    public static BillingCreditExpiryType FromCustom(string value)
    {
        return new BillingCreditExpiryType(value);
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

    public static bool operator ==(BillingCreditExpiryType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditExpiryType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditExpiryType value) => value.Value;

    public static explicit operator BillingCreditExpiryType(string value) => new(value);

    internal class BillingCreditExpiryTypeSerializer : JsonConverter<BillingCreditExpiryType>
    {
        public override BillingCreditExpiryType Read(
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
            return new BillingCreditExpiryType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditExpiryType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditExpiryType ReadAsPropertyName(
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
            return new BillingCreditExpiryType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditExpiryType value,
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
        public const string Duration = "duration";

        public const string EndOfBillingPeriod = "end_of_billing_period";

        public const string EndOfNextBillingPeriod = "end_of_next_billing_period";

        public const string EndOfTrial = "end_of_trial";

        public const string NoExpiry = "no_expiry";
    }
}
