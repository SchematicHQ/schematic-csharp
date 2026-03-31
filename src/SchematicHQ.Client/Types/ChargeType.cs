using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(ChargeType.ChargeTypeSerializer))]
[Serializable]
public readonly record struct ChargeType : IStringEnum
{
    public static readonly ChargeType Free = new(Values.Free);

    public static readonly ChargeType OneTime = new(Values.OneTime);

    public static readonly ChargeType Recurring = new(Values.Recurring);

    public ChargeType(string value)
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
    public static ChargeType FromCustom(string value)
    {
        return new ChargeType(value);
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

    public static bool operator ==(ChargeType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(ChargeType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ChargeType value) => value.Value;

    public static explicit operator ChargeType(string value) => new(value);

    internal class ChargeTypeSerializer : JsonConverter<ChargeType>
    {
        public override ChargeType Read(
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
            return new ChargeType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ChargeType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ChargeType ReadAsPropertyName(
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
            return new ChargeType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ChargeType value,
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
        public const string Free = "free";

        public const string OneTime = "one_time";

        public const string Recurring = "recurring";
    }
}
