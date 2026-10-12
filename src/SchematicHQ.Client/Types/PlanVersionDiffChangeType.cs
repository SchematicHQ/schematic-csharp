using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanVersionDiffChangeType.PlanVersionDiffChangeTypeSerializer))]
[Serializable]
public readonly record struct PlanVersionDiffChangeType : IStringEnum
{
    public static readonly PlanVersionDiffChangeType Added = new(Values.Added);

    public static readonly PlanVersionDiffChangeType Changed = new(Values.Changed);

    public static readonly PlanVersionDiffChangeType Removed = new(Values.Removed);

    public PlanVersionDiffChangeType(string value)
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
    public static PlanVersionDiffChangeType FromCustom(string value)
    {
        return new PlanVersionDiffChangeType(value);
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

    public static bool operator ==(PlanVersionDiffChangeType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanVersionDiffChangeType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanVersionDiffChangeType value) => value.Value;

    public static explicit operator PlanVersionDiffChangeType(string value) => new(value);

    internal class PlanVersionDiffChangeTypeSerializer : JsonConverter<PlanVersionDiffChangeType>
    {
        public override PlanVersionDiffChangeType Read(
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
            return new PlanVersionDiffChangeType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanVersionDiffChangeType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanVersionDiffChangeType ReadAsPropertyName(
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
            return new PlanVersionDiffChangeType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanVersionDiffChangeType value,
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
        public const string Added = "added";

        public const string Changed = "changed";

        public const string Removed = "removed";
    }
}
