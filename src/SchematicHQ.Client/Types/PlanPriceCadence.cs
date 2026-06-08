using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanPriceCadence.PlanPriceCadenceSerializer))]
[Serializable]
public readonly record struct PlanPriceCadence : IStringEnum
{
    public static readonly PlanPriceCadence Monthly = new(Values.Monthly);

    public static readonly PlanPriceCadence Quarterly = new(Values.Quarterly);

    public static readonly PlanPriceCadence Yearly = new(Values.Yearly);

    public PlanPriceCadence(string value)
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
    public static PlanPriceCadence FromCustom(string value)
    {
        return new PlanPriceCadence(value);
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

    public static bool operator ==(PlanPriceCadence value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanPriceCadence value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanPriceCadence value) => value.Value;

    public static explicit operator PlanPriceCadence(string value) => new(value);

    internal class PlanPriceCadenceSerializer : JsonConverter<PlanPriceCadence>
    {
        public override PlanPriceCadence Read(
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
            return new PlanPriceCadence(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanPriceCadence value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanPriceCadence ReadAsPropertyName(
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
            return new PlanPriceCadence(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanPriceCadence value,
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
        public const string Monthly = "monthly";

        public const string Quarterly = "quarterly";

        public const string Yearly = "yearly";
    }
}
