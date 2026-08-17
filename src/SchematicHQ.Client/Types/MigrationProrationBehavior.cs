using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(MigrationProrationBehavior.MigrationProrationBehaviorSerializer))]
[Serializable]
public readonly record struct MigrationProrationBehavior : IStringEnum
{
    public static readonly MigrationProrationBehavior AlwaysInvoice = new(Values.AlwaysInvoice);

    public static readonly MigrationProrationBehavior CreateProrations = new(
        Values.CreateProrations
    );

    public MigrationProrationBehavior(string value)
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
    public static MigrationProrationBehavior FromCustom(string value)
    {
        return new MigrationProrationBehavior(value);
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

    public static bool operator ==(MigrationProrationBehavior value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MigrationProrationBehavior value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MigrationProrationBehavior value) => value.Value;

    public static explicit operator MigrationProrationBehavior(string value) => new(value);

    internal class MigrationProrationBehaviorSerializer : JsonConverter<MigrationProrationBehavior>
    {
        public override MigrationProrationBehavior Read(
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
            return new MigrationProrationBehavior(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MigrationProrationBehavior value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MigrationProrationBehavior ReadAsPropertyName(
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
            return new MigrationProrationBehavior(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MigrationProrationBehavior value,
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
        public const string AlwaysInvoice = "always_invoice";

        public const string CreateProrations = "create_prorations";
    }
}
