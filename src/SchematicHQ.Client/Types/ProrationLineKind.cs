using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(ProrationLineKind.ProrationLineKindSerializer))]
[Serializable]
public readonly record struct ProrationLineKind : IStringEnum
{
    public static readonly ProrationLineKind Charge = new(Values.Charge);

    public static readonly ProrationLineKind Credit = new(Values.Credit);

    public ProrationLineKind(string value)
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
    public static ProrationLineKind FromCustom(string value)
    {
        return new ProrationLineKind(value);
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

    public static bool operator ==(ProrationLineKind value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProrationLineKind value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProrationLineKind value) => value.Value;

    public static explicit operator ProrationLineKind(string value) => new(value);

    internal class ProrationLineKindSerializer : JsonConverter<ProrationLineKind>
    {
        public override ProrationLineKind Read(
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
            return new ProrationLineKind(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProrationLineKind value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ProrationLineKind ReadAsPropertyName(
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
            return new ProrationLineKind(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ProrationLineKind value,
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
        public const string Charge = "charge";

        public const string Credit = "credit";
    }
}
