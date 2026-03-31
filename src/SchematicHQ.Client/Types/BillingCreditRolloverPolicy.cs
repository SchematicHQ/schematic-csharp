using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingCreditRolloverPolicy.BillingCreditRolloverPolicySerializer))]
[Serializable]
public readonly record struct BillingCreditRolloverPolicy : IStringEnum
{
    public static readonly BillingCreditRolloverPolicy Expire = new(Values.Expire);

    public static readonly BillingCreditRolloverPolicy None = new(Values.None);

    public static readonly BillingCreditRolloverPolicy Rollover = new(Values.Rollover);

    public BillingCreditRolloverPolicy(string value)
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
    public static BillingCreditRolloverPolicy FromCustom(string value)
    {
        return new BillingCreditRolloverPolicy(value);
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

    public static bool operator ==(BillingCreditRolloverPolicy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditRolloverPolicy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditRolloverPolicy value) => value.Value;

    public static explicit operator BillingCreditRolloverPolicy(string value) => new(value);

    internal class BillingCreditRolloverPolicySerializer
        : JsonConverter<BillingCreditRolloverPolicy>
    {
        public override BillingCreditRolloverPolicy Read(
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
            return new BillingCreditRolloverPolicy(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditRolloverPolicy value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditRolloverPolicy ReadAsPropertyName(
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
            return new BillingCreditRolloverPolicy(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditRolloverPolicy value,
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
        public const string Expire = "expire";

        public const string None = "none";

        public const string Rollover = "rollover";
    }
}
