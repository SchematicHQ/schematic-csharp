using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingTiersMode.BillingTiersModeSerializer))]
[Serializable]
public readonly record struct BillingTiersMode : IStringEnum
{
    public static readonly BillingTiersMode Graduated = new(Values.Graduated);

    public static readonly BillingTiersMode Volume = new(Values.Volume);

    public BillingTiersMode(string value)
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
    public static BillingTiersMode FromCustom(string value)
    {
        return new BillingTiersMode(value);
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

    public static bool operator ==(BillingTiersMode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingTiersMode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingTiersMode value) => value.Value;

    public static explicit operator BillingTiersMode(string value) => new(value);

    internal class BillingTiersModeSerializer : JsonConverter<BillingTiersMode>
    {
        public override BillingTiersMode Read(
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
            return new BillingTiersMode(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingTiersMode value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingTiersMode ReadAsPropertyName(
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
            return new BillingTiersMode(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingTiersMode value,
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
        public const string Graduated = "graduated";

        public const string Volume = "volume";
    }
}
