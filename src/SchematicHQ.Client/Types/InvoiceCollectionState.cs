using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(InvoiceCollectionState.InvoiceCollectionStateSerializer))]
[Serializable]
public readonly record struct InvoiceCollectionState : IStringEnum
{
    public static readonly InvoiceCollectionState Exported = new(Values.Exported);

    public static readonly InvoiceCollectionState Paid = new(Values.Paid);

    public static readonly InvoiceCollectionState PartiallyPaid = new(Values.PartiallyPaid);

    public static readonly InvoiceCollectionState PastDue = new(Values.PastDue);

    public static readonly InvoiceCollectionState PaymentFailed = new(Values.PaymentFailed);

    public static readonly InvoiceCollectionState Queued = new(Values.Queued);

    public static readonly InvoiceCollectionState Sent = new(Values.Sent);

    public InvoiceCollectionState(string value)
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
    public static InvoiceCollectionState FromCustom(string value)
    {
        return new InvoiceCollectionState(value);
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

    public static bool operator ==(InvoiceCollectionState value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InvoiceCollectionState value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InvoiceCollectionState value) => value.Value;

    public static explicit operator InvoiceCollectionState(string value) => new(value);

    internal class InvoiceCollectionStateSerializer : JsonConverter<InvoiceCollectionState>
    {
        public override InvoiceCollectionState Read(
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
            return new InvoiceCollectionState(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InvoiceCollectionState value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override InvoiceCollectionState ReadAsPropertyName(
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
            return new InvoiceCollectionState(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            InvoiceCollectionState value,
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
        public const string Exported = "exported";

        public const string Paid = "paid";

        public const string PartiallyPaid = "partially_paid";

        public const string PastDue = "past_due";

        public const string PaymentFailed = "payment_failed";

        public const string Queued = "queued";

        public const string Sent = "sent";
    }
}
