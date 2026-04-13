using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(IntegrationType.IntegrationTypeSerializer))]
[Serializable]
public readonly record struct IntegrationType : IStringEnum
{
    public static readonly IntegrationType Clerk = new(Values.Clerk);

    public static readonly IntegrationType Orb = new(Values.Orb);

    public static readonly IntegrationType Stripe = new(Values.Stripe);

    public static readonly IntegrationType Unknown = new(Values.Unknown);

    public IntegrationType(string value)
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
    public static IntegrationType FromCustom(string value)
    {
        return new IntegrationType(value);
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

    public static bool operator ==(IntegrationType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(IntegrationType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(IntegrationType value) => value.Value;

    public static explicit operator IntegrationType(string value) => new(value);

    internal class IntegrationTypeSerializer : JsonConverter<IntegrationType>
    {
        public override IntegrationType Read(
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
            return new IntegrationType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            IntegrationType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override IntegrationType ReadAsPropertyName(
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
            return new IntegrationType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            IntegrationType value,
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
        public const string Clerk = "clerk";

        public const string Orb = "orb";

        public const string Stripe = "stripe";

        public const string Unknown = "unknown";
    }
}
