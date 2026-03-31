using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditGrantSortOrder.CreditGrantSortOrderSerializer))]
[Serializable]
public readonly record struct CreditGrantSortOrder : IStringEnum
{
    public static readonly CreditGrantSortOrder CreatedAt = new(Values.CreatedAt);

    public static readonly CreditGrantSortOrder ExpiresAt = new(Values.ExpiresAt);

    public static readonly CreditGrantSortOrder Quantity = new(Values.Quantity);

    public static readonly CreditGrantSortOrder QuantityRemaining = new(Values.QuantityRemaining);

    public CreditGrantSortOrder(string value)
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
    public static CreditGrantSortOrder FromCustom(string value)
    {
        return new CreditGrantSortOrder(value);
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

    public static bool operator ==(CreditGrantSortOrder value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditGrantSortOrder value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditGrantSortOrder value) => value.Value;

    public static explicit operator CreditGrantSortOrder(string value) => new(value);

    internal class CreditGrantSortOrderSerializer : JsonConverter<CreditGrantSortOrder>
    {
        public override CreditGrantSortOrder Read(
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
            return new CreditGrantSortOrder(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditGrantSortOrder value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditGrantSortOrder ReadAsPropertyName(
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
            return new CreditGrantSortOrder(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditGrantSortOrder value,
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
        public const string CreatedAt = "created_at";

        public const string ExpiresAt = "expires_at";

        public const string Quantity = "quantity";

        public const string QuantityRemaining = "quantity_remaining";
    }
}
