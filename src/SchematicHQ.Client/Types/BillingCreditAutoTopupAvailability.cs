using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(BillingCreditAutoTopupAvailability.BillingCreditAutoTopupAvailabilitySerializer)
)]
[Serializable]
public readonly record struct BillingCreditAutoTopupAvailability : IStringEnum
{
    public static readonly BillingCreditAutoTopupAvailability Off = new(Values.Off);

    public static readonly BillingCreditAutoTopupAvailability Automatic = new(Values.Automatic);

    public static readonly BillingCreditAutoTopupAvailability UserControlled = new(
        Values.UserControlled
    );

    public BillingCreditAutoTopupAvailability(string value)
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
    public static BillingCreditAutoTopupAvailability FromCustom(string value)
    {
        return new BillingCreditAutoTopupAvailability(value);
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

    public static bool operator ==(BillingCreditAutoTopupAvailability value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditAutoTopupAvailability value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditAutoTopupAvailability value) => value.Value;

    public static explicit operator BillingCreditAutoTopupAvailability(string value) => new(value);

    internal class BillingCreditAutoTopupAvailabilitySerializer
        : JsonConverter<BillingCreditAutoTopupAvailability>
    {
        public override BillingCreditAutoTopupAvailability Read(
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
            return new BillingCreditAutoTopupAvailability(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditAutoTopupAvailability value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditAutoTopupAvailability ReadAsPropertyName(
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
            return new BillingCreditAutoTopupAvailability(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditAutoTopupAvailability value,
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
        public const string Off = "off";

        public const string Automatic = "automatic";

        public const string UserControlled = "user_controlled";
    }
}
