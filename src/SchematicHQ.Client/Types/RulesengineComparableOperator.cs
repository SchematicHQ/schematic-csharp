using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RulesengineComparableOperator.RulesengineComparableOperatorSerializer))]
[Serializable]
public readonly record struct RulesengineComparableOperator : IStringEnum
{
    public static readonly RulesengineComparableOperator Eq = new(Values.Eq);

    public static readonly RulesengineComparableOperator Gt = new(Values.Gt);

    public static readonly RulesengineComparableOperator Gte = new(Values.Gte);

    public static readonly RulesengineComparableOperator IsEmpty = new(Values.IsEmpty);

    public static readonly RulesengineComparableOperator Lt = new(Values.Lt);

    public static readonly RulesengineComparableOperator Lte = new(Values.Lte);

    public static readonly RulesengineComparableOperator NotEmpty = new(Values.NotEmpty);

    public static readonly RulesengineComparableOperator Ne = new(Values.Ne);

    public RulesengineComparableOperator(string value)
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
    public static RulesengineComparableOperator FromCustom(string value)
    {
        return new RulesengineComparableOperator(value);
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

    public static bool operator ==(RulesengineComparableOperator value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineComparableOperator value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineComparableOperator value) => value.Value;

    public static explicit operator RulesengineComparableOperator(string value) => new(value);

    internal class RulesengineComparableOperatorSerializer
        : JsonConverter<RulesengineComparableOperator>
    {
        public override RulesengineComparableOperator Read(
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
            return new RulesengineComparableOperator(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineComparableOperator value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineComparableOperator ReadAsPropertyName(
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
            return new RulesengineComparableOperator(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineComparableOperator value,
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
