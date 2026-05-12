using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(IntegrationState.IntegrationStateSerializer))]
[Serializable]
public readonly record struct IntegrationState : IStringEnum
{
    public static readonly IntegrationState Active = new(Values.Active);

    public static readonly IntegrationState Created = new(Values.Created);

    public static readonly IntegrationState Pending = new(Values.Pending);

    public IntegrationState(string value)
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
    public static IntegrationState FromCustom(string value)
    {
        return new IntegrationState(value);
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

    public static bool operator ==(IntegrationState value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(IntegrationState value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(IntegrationState value) => value.Value;

    public static explicit operator IntegrationState(string value) => new(value);

    internal class IntegrationStateSerializer : JsonConverter<IntegrationState>
    {
        public override IntegrationState Read(
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
            return new IntegrationState(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            IntegrationState value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override IntegrationState ReadAsPropertyName(
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
            return new IntegrationState(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            IntegrationState value,
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
        public const string Active = "active";

        public const string Created = "created";

        public const string Pending = "pending";
    }
}
