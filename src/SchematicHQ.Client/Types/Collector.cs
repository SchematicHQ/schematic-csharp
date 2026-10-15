using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(Collector.CollectorSerializer))]
[Serializable]
public readonly record struct Collector : IStringEnum
{
    public static readonly Collector External = new(Values.External);

    public static readonly Collector StripeInvoice = new(Values.StripeInvoice);

    public static readonly Collector StripePayments = new(Values.StripePayments);

    public Collector(string value)
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
    public static Collector FromCustom(string value)
    {
        return new Collector(value);
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

    public static bool operator ==(Collector value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Collector value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Collector value) => value.Value;

    public static explicit operator Collector(string value) => new(value);

    internal class CollectorSerializer : JsonConverter<Collector>
    {
        public override Collector Read(
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
            return new Collector(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Collector value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override Collector ReadAsPropertyName(
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
            return new Collector(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Collector value,
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
        public const string External = "external";

        public const string StripeInvoice = "stripe_invoice";

        public const string StripePayments = "stripe_payments";
    }
}
