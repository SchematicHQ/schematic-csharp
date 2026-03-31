using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditLedgerPeriod.CreditLedgerPeriodSerializer))]
[Serializable]
public readonly record struct CreditLedgerPeriod : IStringEnum
{
    public static readonly CreditLedgerPeriod Daily = new(Values.Daily);

    public static readonly CreditLedgerPeriod Monthly = new(Values.Monthly);

    public static readonly CreditLedgerPeriod Raw = new(Values.Raw);

    public static readonly CreditLedgerPeriod Weekly = new(Values.Weekly);

    public CreditLedgerPeriod(string value)
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
    public static CreditLedgerPeriod FromCustom(string value)
    {
        return new CreditLedgerPeriod(value);
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

    public static bool operator ==(CreditLedgerPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditLedgerPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditLedgerPeriod value) => value.Value;

    public static explicit operator CreditLedgerPeriod(string value) => new(value);

    internal class CreditLedgerPeriodSerializer : JsonConverter<CreditLedgerPeriod>
    {
        public override CreditLedgerPeriod Read(
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
            return new CreditLedgerPeriod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditLedgerPeriod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditLedgerPeriod ReadAsPropertyName(
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
            return new CreditLedgerPeriod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditLedgerPeriod value,
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
        public const string Daily = "daily";

        public const string Monthly = "monthly";

        public const string Raw = "raw";

        public const string Weekly = "weekly";
    }
}
