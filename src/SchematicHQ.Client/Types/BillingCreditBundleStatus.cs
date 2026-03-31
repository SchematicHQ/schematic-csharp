using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingCreditBundleStatus.BillingCreditBundleStatusSerializer))]
[Serializable]
public readonly record struct BillingCreditBundleStatus : IStringEnum
{
    public static readonly BillingCreditBundleStatus Active = new(Values.Active);

    public static readonly BillingCreditBundleStatus Inactive = new(Values.Inactive);

    public BillingCreditBundleStatus(string value)
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
    public static BillingCreditBundleStatus FromCustom(string value)
    {
        return new BillingCreditBundleStatus(value);
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

    public static bool operator ==(BillingCreditBundleStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditBundleStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditBundleStatus value) => value.Value;

    public static explicit operator BillingCreditBundleStatus(string value) => new(value);

    internal class BillingCreditBundleStatusSerializer : JsonConverter<BillingCreditBundleStatus>
    {
        public override BillingCreditBundleStatus Read(
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
            return new BillingCreditBundleStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditBundleStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditBundleStatus ReadAsPropertyName(
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
            return new BillingCreditBundleStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditBundleStatus value,
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

        public const string Inactive = "inactive";
    }
}
