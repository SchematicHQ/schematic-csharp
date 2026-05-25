using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingStrategy.BillingStrategySerializer))]
[Serializable]
public readonly record struct BillingStrategy : IStringEnum
{
    public static readonly BillingStrategy SchematicManaged = new(Values.SchematicManaged);

    public static readonly BillingStrategy ProviderManaged = new(Values.ProviderManaged);

    public static readonly BillingStrategy NoBilling = new(Values.NoBilling);

    public BillingStrategy(string value)
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
    public static BillingStrategy FromCustom(string value)
    {
        return new BillingStrategy(value);
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

    public static bool operator ==(BillingStrategy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingStrategy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingStrategy value) => value.Value;

    public static explicit operator BillingStrategy(string value) => new(value);

    internal class BillingStrategySerializer : JsonConverter<BillingStrategy>
    {
        public override BillingStrategy Read(
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
            return new BillingStrategy(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingStrategy value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingStrategy ReadAsPropertyName(
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
            return new BillingStrategy(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingStrategy value,
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
        public const string SchematicManaged = "schematic_managed";

        public const string ProviderManaged = "provider_managed";

        public const string NoBilling = "no_billing";
    }
}
