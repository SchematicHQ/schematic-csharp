using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditTransferReason.CreditTransferReasonSerializer))]
[Serializable]
public readonly record struct CreditTransferReason : IStringEnum
{
    public static readonly CreditTransferReason OverdraftRecovery = new(Values.OverdraftRecovery);

    public static readonly CreditTransferReason PostpaidDebtMoved = new(Values.PostpaidDebtMoved);

    public static readonly CreditTransferReason PostpaidForgiven = new(Values.PostpaidForgiven);

    public static readonly CreditTransferReason PostpaidPaid = new(Values.PostpaidPaid);

    public CreditTransferReason(string value)
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
    public static CreditTransferReason FromCustom(string value)
    {
        return new CreditTransferReason(value);
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

    public static bool operator ==(CreditTransferReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditTransferReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditTransferReason value) => value.Value;

    public static explicit operator CreditTransferReason(string value) => new(value);

    internal class CreditTransferReasonSerializer : JsonConverter<CreditTransferReason>
    {
        public override CreditTransferReason Read(
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
            return new CreditTransferReason(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditTransferReason value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditTransferReason ReadAsPropertyName(
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
            return new CreditTransferReason(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditTransferReason value,
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
        public const string OverdraftRecovery = "overdraft_recovery";

        public const string PostpaidDebtMoved = "postpaid_debt_moved";

        public const string PostpaidForgiven = "postpaid_forgiven";

        public const string PostpaidPaid = "postpaid_paid";
    }
}
