using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EntitlementValueType.EntitlementValueTypeSerializer))]
[Serializable]
public readonly record struct EntitlementValueType : IStringEnum
{
    public static readonly EntitlementValueType Boolean = new(Values.Boolean);

    public static readonly EntitlementValueType Credit = new(Values.Credit);

    public static readonly EntitlementValueType Numeric = new(Values.Numeric);

    public static readonly EntitlementValueType Trait = new(Values.Trait);

    public static readonly EntitlementValueType Unknown = new(Values.Unknown);

    public static readonly EntitlementValueType Unlimited = new(Values.Unlimited);

    public EntitlementValueType(string value)
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
    public static EntitlementValueType FromCustom(string value)
    {
        return new EntitlementValueType(value);
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

    public static bool operator ==(EntitlementValueType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EntitlementValueType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EntitlementValueType value) => value.Value;

    public static explicit operator EntitlementValueType(string value) => new(value);

    internal class EntitlementValueTypeSerializer : JsonConverter<EntitlementValueType>
    {
        public override EntitlementValueType Read(
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
            return new EntitlementValueType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            EntitlementValueType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override EntitlementValueType ReadAsPropertyName(
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
            return new EntitlementValueType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            EntitlementValueType value,
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
        public const string Boolean = "boolean";

        public const string Credit = "credit";

        public const string Numeric = "numeric";

        public const string Trait = "trait";

        public const string Unknown = "unknown";

        public const string Unlimited = "unlimited";
    }
}
