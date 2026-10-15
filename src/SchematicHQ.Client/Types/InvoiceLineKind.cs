using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(InvoiceLineKind.InvoiceLineKindSerializer))]
[Serializable]
public readonly record struct InvoiceLineKind : IStringEnum
{
    public static readonly InvoiceLineKind Adjustment = new(Values.Adjustment);

    public static readonly InvoiceLineKind Purchase = new(Values.Purchase);

    public static readonly InvoiceLineKind Reconciliation = new(Values.Reconciliation);

    public static readonly InvoiceLineKind Subscription = new(Values.Subscription);

    public static readonly InvoiceLineKind TrueUp = new(Values.TrueUp);

    public InvoiceLineKind(string value)
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
    public static InvoiceLineKind FromCustom(string value)
    {
        return new InvoiceLineKind(value);
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

    public static bool operator ==(InvoiceLineKind value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InvoiceLineKind value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InvoiceLineKind value) => value.Value;

    public static explicit operator InvoiceLineKind(string value) => new(value);

    internal class InvoiceLineKindSerializer : JsonConverter<InvoiceLineKind>
    {
        public override InvoiceLineKind Read(
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
            return new InvoiceLineKind(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InvoiceLineKind value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override InvoiceLineKind ReadAsPropertyName(
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
            return new InvoiceLineKind(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            InvoiceLineKind value,
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
        public const string Adjustment = "adjustment";

        public const string Purchase = "purchase";

        public const string Reconciliation = "reconciliation";

        public const string Subscription = "subscription";

        public const string TrueUp = "true_up";
    }
}
