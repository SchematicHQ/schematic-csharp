using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditLedgerSourceType.CreditLedgerSourceTypeSerializer))]
[Serializable]
public readonly record struct CreditLedgerSourceType : IStringEnum
{
    public static readonly CreditLedgerSourceType BillingCharge = new(Values.BillingCharge);

    public static readonly CreditLedgerSourceType BillingCreditGrant = new(
        Values.BillingCreditGrant
    );

    public static readonly CreditLedgerSourceType BillingCreditTransfer = new(
        Values.BillingCreditTransfer
    );

    public static readonly CreditLedgerSourceType BillingSettlement = new(Values.BillingSettlement);

    public CreditLedgerSourceType(string value)
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
    public static CreditLedgerSourceType FromCustom(string value)
    {
        return new CreditLedgerSourceType(value);
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

    public static bool operator ==(CreditLedgerSourceType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditLedgerSourceType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditLedgerSourceType value) => value.Value;

    public static explicit operator CreditLedgerSourceType(string value) => new(value);

    internal class CreditLedgerSourceTypeSerializer : JsonConverter<CreditLedgerSourceType>
    {
        public override CreditLedgerSourceType Read(
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
            return new CreditLedgerSourceType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditLedgerSourceType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditLedgerSourceType ReadAsPropertyName(
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
            return new CreditLedgerSourceType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditLedgerSourceType value,
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
        public const string BillingCharge = "billing_charge";

        public const string BillingCreditGrant = "billing_credit_grant";

        public const string BillingCreditTransfer = "billing_credit_transfer";

        public const string BillingSettlement = "billing_settlement";
    }
}
