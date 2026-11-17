using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CreditLedgerExportMetadataEventType.CreditLedgerExportMetadataEventTypeSerializer)
)]
[Serializable]
public readonly record struct CreditLedgerExportMetadataEventType : IStringEnum
{
    public static readonly CreditLedgerExportMetadataEventType Adjustment = new(Values.Adjustment);

    public static readonly CreditLedgerExportMetadataEventType Charge = new(Values.Charge);

    public static readonly CreditLedgerExportMetadataEventType Grant = new(Values.Grant);

    public static readonly CreditLedgerExportMetadataEventType Settlement = new(Values.Settlement);

    public static readonly CreditLedgerExportMetadataEventType Transfer = new(Values.Transfer);

    public static readonly CreditLedgerExportMetadataEventType Usage = new(Values.Usage);

    public static readonly CreditLedgerExportMetadataEventType ZeroOut = new(Values.ZeroOut);

    public CreditLedgerExportMetadataEventType(string value)
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
    public static CreditLedgerExportMetadataEventType FromCustom(string value)
    {
        return new CreditLedgerExportMetadataEventType(value);
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

    public static bool operator ==(CreditLedgerExportMetadataEventType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditLedgerExportMetadataEventType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditLedgerExportMetadataEventType value) =>
        value.Value;

    public static explicit operator CreditLedgerExportMetadataEventType(string value) => new(value);

    internal class CreditLedgerExportMetadataEventTypeSerializer
        : JsonConverter<CreditLedgerExportMetadataEventType>
    {
        public override CreditLedgerExportMetadataEventType Read(
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
            return new CreditLedgerExportMetadataEventType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditLedgerExportMetadataEventType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditLedgerExportMetadataEventType ReadAsPropertyName(
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
            return new CreditLedgerExportMetadataEventType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditLedgerExportMetadataEventType value,
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

        public const string Grant = "grant";

        public const string Settlement = "settlement";

        public const string Transfer = "transfer";

        public const string Usage = "usage";

        public const string ZeroOut = "zero_out";
    }
}
