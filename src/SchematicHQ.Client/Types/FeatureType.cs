using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(FeatureType.FeatureTypeSerializer))]
[Serializable]
public readonly record struct FeatureType : IStringEnum
{
    public static readonly FeatureType Boolean = new(Values.Boolean);

    public static readonly FeatureType Event = new(Values.Event);

    public static readonly FeatureType Trait = new(Values.Trait);

    public FeatureType(string value)
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
    public static FeatureType FromCustom(string value)
    {
        return new FeatureType(value);
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

    public static bool operator ==(FeatureType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FeatureType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FeatureType value) => value.Value;

    public static explicit operator FeatureType(string value) => new(value);

    internal class FeatureTypeSerializer : JsonConverter<FeatureType>
    {
        public override FeatureType Read(
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
            return new FeatureType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FeatureType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override FeatureType ReadAsPropertyName(
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
            return new FeatureType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            FeatureType value,
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

        public const string Event = "event";

        public const string Trait = "trait";
    }
}
