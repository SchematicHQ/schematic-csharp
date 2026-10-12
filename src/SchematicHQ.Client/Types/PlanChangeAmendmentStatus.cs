using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanChangeAmendmentStatus.PlanChangeAmendmentStatusSerializer))]
[Serializable]
public readonly record struct PlanChangeAmendmentStatus : IStringEnum
{
    public static readonly PlanChangeAmendmentStatus Applied = new(Values.Applied);

    public static readonly PlanChangeAmendmentStatus Cancelled = new(Values.Cancelled);

    public static readonly PlanChangeAmendmentStatus Failed = new(Values.Failed);

    public static readonly PlanChangeAmendmentStatus Scheduled = new(Values.Scheduled);

    public static readonly PlanChangeAmendmentStatus Skipped = new(Values.Skipped);

    public PlanChangeAmendmentStatus(string value)
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
    public static PlanChangeAmendmentStatus FromCustom(string value)
    {
        return new PlanChangeAmendmentStatus(value);
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

    public static bool operator ==(PlanChangeAmendmentStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeAmendmentStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeAmendmentStatus value) => value.Value;

    public static explicit operator PlanChangeAmendmentStatus(string value) => new(value);

    internal class PlanChangeAmendmentStatusSerializer : JsonConverter<PlanChangeAmendmentStatus>
    {
        public override PlanChangeAmendmentStatus Read(
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
            return new PlanChangeAmendmentStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanChangeAmendmentStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanChangeAmendmentStatus ReadAsPropertyName(
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
            return new PlanChangeAmendmentStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanChangeAmendmentStatus value,
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
        public const string Applied = "applied";

        public const string Cancelled = "cancelled";

        public const string Failed = "failed";

        public const string Scheduled = "scheduled";

        public const string Skipped = "skipped";
    }
}
