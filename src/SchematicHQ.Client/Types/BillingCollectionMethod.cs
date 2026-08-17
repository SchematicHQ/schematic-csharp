using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingCollectionMethod.BillingCollectionMethodSerializer))]
[Serializable]
public readonly record struct BillingCollectionMethod : IStringEnum
{
    public static readonly BillingCollectionMethod ChargeAutomatically = new(
        Values.ChargeAutomatically
    );

    public static readonly BillingCollectionMethod SendInvoice = new(Values.SendInvoice);

    public BillingCollectionMethod(string value)
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
    public static BillingCollectionMethod FromCustom(string value)
    {
        return new BillingCollectionMethod(value);
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

    public static bool operator ==(BillingCollectionMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCollectionMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCollectionMethod value) => value.Value;

    public static explicit operator BillingCollectionMethod(string value) => new(value);

    internal class BillingCollectionMethodSerializer : JsonConverter<BillingCollectionMethod>
    {
        public override BillingCollectionMethod Read(
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
            return new BillingCollectionMethod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCollectionMethod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCollectionMethod ReadAsPropertyName(
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
            return new BillingCollectionMethod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCollectionMethod value,
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
        public const string ChargeAutomatically = "charge_automatically";

        public const string SendInvoice = "send_invoice";
    }
}
