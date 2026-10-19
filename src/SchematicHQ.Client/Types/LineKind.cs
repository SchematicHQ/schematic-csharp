using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(LineKind.LineKindSerializer))]
[Serializable]
public readonly record struct LineKind : IStringEnum
{
    public static readonly LineKind Allowance = new(Values.Allowance);

    public static readonly LineKind Capacity = new(Values.Capacity);

    public static readonly LineKind Rate = new(Values.Rate);

    public LineKind(string value)
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
    public static LineKind FromCustom(string value)
    {
        return new LineKind(value);
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

    public static bool operator ==(LineKind value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(LineKind value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(LineKind value) => value.Value;

    public static explicit operator LineKind(string value) => new(value);

    internal class LineKindSerializer : JsonConverter<LineKind>
    {
        public override LineKind Read(
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
            return new LineKind(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            LineKind value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override LineKind ReadAsPropertyName(
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
            return new LineKind(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            LineKind value,
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
        public const string Allowance = "allowance";

        public const string Capacity = "capacity";

        public const string Rate = "rate";
    }
}
