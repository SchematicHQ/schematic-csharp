using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditLedgerEntryKind.CreditLedgerEntryKindSerializer))]
[Serializable]
public readonly record struct CreditLedgerEntryKind : IStringEnum
{
    public static readonly CreditLedgerEntryKind Adjustment = new(Values.Adjustment);

    public static readonly CreditLedgerEntryKind Charge = new(Values.Charge);

    public static readonly CreditLedgerEntryKind Drawdown = new(Values.Drawdown);

    public static readonly CreditLedgerEntryKind Grant = new(Values.Grant);

    public static readonly CreditLedgerEntryKind Settlement = new(Values.Settlement);

    public static readonly CreditLedgerEntryKind Transfer = new(Values.Transfer);

    public static readonly CreditLedgerEntryKind ZeroOut = new(Values.ZeroOut);

    public CreditLedgerEntryKind(string value)
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
    public static CreditLedgerEntryKind FromCustom(string value)
    {
        return new CreditLedgerEntryKind(value);
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

    public static bool operator ==(CreditLedgerEntryKind value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditLedgerEntryKind value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditLedgerEntryKind value) => value.Value;

    public static explicit operator CreditLedgerEntryKind(string value) => new(value);

    internal class CreditLedgerEntryKindSerializer : JsonConverter<CreditLedgerEntryKind>
    {
        public override CreditLedgerEntryKind Read(
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
            return new CreditLedgerEntryKind(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditLedgerEntryKind value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditLedgerEntryKind ReadAsPropertyName(
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
            return new CreditLedgerEntryKind(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditLedgerEntryKind value,
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
        public const string Adjustment = "adjustment";

        public const string Charge = "charge";

        public const string Drawdown = "drawdown";

        public const string Grant = "grant";

        public const string Settlement = "settlement";

        public const string Transfer = "transfer";

        public const string ZeroOut = "zero_out";
    }
}
