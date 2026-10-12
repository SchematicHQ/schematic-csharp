using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(FeatureEventSource.FeatureEventSourceSerializer))]
[Serializable]
public readonly record struct FeatureEventSource : IStringEnum
{
    public static readonly FeatureEventSource Inference = new(Values.Inference);

    public static readonly FeatureEventSource Track = new(Values.Track);

    public FeatureEventSource(string value)
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
    public static FeatureEventSource FromCustom(string value)
    {
        return new FeatureEventSource(value);
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

    public static bool operator ==(FeatureEventSource value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FeatureEventSource value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FeatureEventSource value) => value.Value;

    public static explicit operator FeatureEventSource(string value) => new(value);

    internal class FeatureEventSourceSerializer : JsonConverter<FeatureEventSource>
    {
        public override FeatureEventSource Read(
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
            return new FeatureEventSource(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FeatureEventSource value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override FeatureEventSource ReadAsPropertyName(
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
            return new FeatureEventSource(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            FeatureEventSource value,
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
        public const string Inference = "inference";

        public const string Track = "track";
    }
}
