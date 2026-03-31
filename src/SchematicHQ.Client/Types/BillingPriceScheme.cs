using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingPriceScheme.BillingPriceSchemeSerializer))]
[Serializable]
public readonly record struct BillingPriceScheme : IStringEnum
{
    public static readonly BillingPriceScheme PerUnit = new(Values.PerUnit);

    public static readonly BillingPriceScheme Tiered = new(Values.Tiered);

    public BillingPriceScheme(string value)
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
    public static BillingPriceScheme FromCustom(string value)
    {
        return new BillingPriceScheme(value);
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

    public static bool operator ==(BillingPriceScheme value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingPriceScheme value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingPriceScheme value) => value.Value;

    public static explicit operator BillingPriceScheme(string value) => new(value);

    internal class BillingPriceSchemeSerializer : JsonConverter<BillingPriceScheme>
    {
        public override BillingPriceScheme Read(
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
            return new BillingPriceScheme(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingPriceScheme value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingPriceScheme ReadAsPropertyName(
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
            return new BillingPriceScheme(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingPriceScheme value,
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
        public const string PerUnit = "per_unit";

        public const string Tiered = "tiered";
    }
}
