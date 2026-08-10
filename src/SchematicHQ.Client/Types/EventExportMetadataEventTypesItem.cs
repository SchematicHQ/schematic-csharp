using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(EventExportMetadataEventTypesItem.EventExportMetadataEventTypesItemSerializer)
)]
[Serializable]
public readonly record struct EventExportMetadataEventTypesItem : IStringEnum
{
    public static readonly EventExportMetadataEventTypesItem FlagCheck = new(Values.FlagCheck);

    public static readonly EventExportMetadataEventTypesItem Identify = new(Values.Identify);

    public static readonly EventExportMetadataEventTypesItem Inference = new(Values.Inference);

    public static readonly EventExportMetadataEventTypesItem Track = new(Values.Track);

    public EventExportMetadataEventTypesItem(string value)
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
    public static EventExportMetadataEventTypesItem FromCustom(string value)
    {
        return new EventExportMetadataEventTypesItem(value);
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

    public static bool operator ==(EventExportMetadataEventTypesItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EventExportMetadataEventTypesItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EventExportMetadataEventTypesItem value) => value.Value;

    public static explicit operator EventExportMetadataEventTypesItem(string value) => new(value);

    internal class EventExportMetadataEventTypesItemSerializer
        : JsonConverter<EventExportMetadataEventTypesItem>
    {
        public override EventExportMetadataEventTypesItem Read(
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
            return new EventExportMetadataEventTypesItem(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            EventExportMetadataEventTypesItem value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override EventExportMetadataEventTypesItem ReadAsPropertyName(
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
            return new EventExportMetadataEventTypesItem(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            EventExportMetadataEventTypesItem value,
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
        public const string FlagCheck = "flag_check";

        public const string Identify = "identify";

        public const string Inference = "inference";

        public const string Track = "track";
    }
}
