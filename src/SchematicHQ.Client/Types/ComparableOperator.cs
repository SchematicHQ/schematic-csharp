using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(ComparableOperator.ComparableOperatorSerializer))]
[Serializable]
public readonly record struct ComparableOperator : IStringEnum
{
    public static readonly ComparableOperator Eq = new(Values.Eq);

    public static readonly ComparableOperator Gt = new(Values.Gt);

    public static readonly ComparableOperator Gte = new(Values.Gte);

    public static readonly ComparableOperator IsEmpty = new(Values.IsEmpty);

    public static readonly ComparableOperator Lt = new(Values.Lt);

    public static readonly ComparableOperator Lte = new(Values.Lte);

    public static readonly ComparableOperator NotEmpty = new(Values.NotEmpty);

    public static readonly ComparableOperator Ne = new(Values.Ne);

    public ComparableOperator(string value)
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
    public static ComparableOperator FromCustom(string value)
    {
        return new ComparableOperator(value);
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

    public static bool operator ==(ComparableOperator value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ComparableOperator value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ComparableOperator value) => value.Value;

    public static explicit operator ComparableOperator(string value) => new(value);

    internal class ComparableOperatorSerializer : JsonConverter<ComparableOperator>
    {
        public override ComparableOperator Read(
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
            return new ComparableOperator(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ComparableOperator value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ComparableOperator ReadAsPropertyName(
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
            return new ComparableOperator(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ComparableOperator value,
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
        public const string Eq = "eq";

        public const string Gt = "gt";

        public const string Gte = "gte";

        public const string IsEmpty = "is_empty";

        public const string Lt = "lt";

        public const string Lte = "lte";

        public const string NotEmpty = "not_empty";

        public const string Ne = "ne";
    }
}
