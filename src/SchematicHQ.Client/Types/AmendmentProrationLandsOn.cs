using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(AmendmentProrationLandsOn.AmendmentProrationLandsOnSerializer))]
[Serializable]
public readonly record struct AmendmentProrationLandsOn : IStringEnum
{
    public static readonly AmendmentProrationLandsOn ImmediateInvoice = new(
        Values.ImmediateInvoice
    );

    public static readonly AmendmentProrationLandsOn NextInvoice = new(Values.NextInvoice);

    public static readonly AmendmentProrationLandsOn None = new(Values.None);

    public AmendmentProrationLandsOn(string value)
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
    public static AmendmentProrationLandsOn FromCustom(string value)
    {
        return new AmendmentProrationLandsOn(value);
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

    public static bool operator ==(AmendmentProrationLandsOn value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AmendmentProrationLandsOn value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AmendmentProrationLandsOn value) => value.Value;

    public static explicit operator AmendmentProrationLandsOn(string value) => new(value);

    internal class AmendmentProrationLandsOnSerializer : JsonConverter<AmendmentProrationLandsOn>
    {
        public override AmendmentProrationLandsOn Read(
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
            return new AmendmentProrationLandsOn(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AmendmentProrationLandsOn value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AmendmentProrationLandsOn ReadAsPropertyName(
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
            return new AmendmentProrationLandsOn(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AmendmentProrationLandsOn value,
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
        public const string ImmediateInvoice = "immediate_invoice";

        public const string NextInvoice = "next_invoice";

        public const string None = "none";
    }
}
