using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(ChargeTiming.ChargeTimingSerializer))]
[Serializable]
public readonly record struct ChargeTiming : IStringEnum
{
    public static readonly ChargeTiming InAdvance = new(Values.InAdvance);

    public static readonly ChargeTiming InArrears = new(Values.InArrears);

    public ChargeTiming(string value)
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
    public static ChargeTiming FromCustom(string value)
    {
        return new ChargeTiming(value);
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

    public static bool operator ==(ChargeTiming value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ChargeTiming value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ChargeTiming value) => value.Value;

    public static explicit operator ChargeTiming(string value) => new(value);

    internal class ChargeTimingSerializer : JsonConverter<ChargeTiming>
    {
        public override ChargeTiming Read(
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
            return new ChargeTiming(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ChargeTiming value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ChargeTiming ReadAsPropertyName(
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
            return new ChargeTiming(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ChargeTiming value,
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
        public const string InAdvance = "in_advance";

        public const string InArrears = "in_arrears";
    }
}
