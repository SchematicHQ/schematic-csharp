using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditEventType.CreditEventTypeSerializer))]
[Serializable]
public readonly record struct CreditEventType : IStringEnum
{
    public static readonly CreditEventType Grant = new(Values.Grant);

    public static readonly CreditEventType Transfer = new(Values.Transfer);

    public static readonly CreditEventType Usage = new(Values.Usage);

    public static readonly CreditEventType ZeroOut = new(Values.ZeroOut);

    public CreditEventType(string value)
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
    public static CreditEventType FromCustom(string value)
    {
        return new CreditEventType(value);
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

    public static bool operator ==(CreditEventType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditEventType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditEventType value) => value.Value;

    public static explicit operator CreditEventType(string value) => new(value);

    internal class CreditEventTypeSerializer : JsonConverter<CreditEventType>
    {
        public override CreditEventType Read(
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
            return new CreditEventType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditEventType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditEventType ReadAsPropertyName(
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
            return new CreditEventType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditEventType value,
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
        public const string Grant = "grant";

        public const string Transfer = "transfer";

        public const string Usage = "usage";

        public const string ZeroOut = "zero_out";
    }
}
