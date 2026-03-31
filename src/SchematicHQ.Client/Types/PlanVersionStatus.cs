using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanVersionStatus.PlanVersionStatusSerializer))]
[Serializable]
public readonly record struct PlanVersionStatus : IStringEnum
{
    public static readonly PlanVersionStatus Published = new(Values.Published);

    public static readonly PlanVersionStatus Draft = new(Values.Draft);

    public static readonly PlanVersionStatus Archived = new(Values.Archived);

    public PlanVersionStatus(string value)
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
    public static PlanVersionStatus FromCustom(string value)
    {
        return new PlanVersionStatus(value);
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

    public static bool operator ==(PlanVersionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanVersionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanVersionStatus value) => value.Value;

    public static explicit operator PlanVersionStatus(string value) => new(value);

    internal class PlanVersionStatusSerializer : JsonConverter<PlanVersionStatus>
    {
        public override PlanVersionStatus Read(
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
            return new PlanVersionStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanVersionStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanVersionStatus ReadAsPropertyName(
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
            return new PlanVersionStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanVersionStatus value,
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
        public const string Published = "published";

        public const string Draft = "draft";

        public const string Archived = "archived";
    }
}
