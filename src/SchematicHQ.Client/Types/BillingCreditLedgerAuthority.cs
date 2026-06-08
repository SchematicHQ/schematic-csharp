using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingCreditLedgerAuthority.BillingCreditLedgerAuthoritySerializer))]
[Serializable]
public readonly record struct BillingCreditLedgerAuthority : IStringEnum
{
    public static readonly BillingCreditLedgerAuthority SchematicAuthoritative = new(
        Values.SchematicAuthoritative
    );

    public static readonly BillingCreditLedgerAuthority ExternalMirror = new(Values.ExternalMirror);

    public static readonly BillingCreditLedgerAuthority ExternalRated = new(Values.ExternalRated);

    public BillingCreditLedgerAuthority(string value)
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
    public static BillingCreditLedgerAuthority FromCustom(string value)
    {
        return new BillingCreditLedgerAuthority(value);
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

    public static bool operator ==(BillingCreditLedgerAuthority value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditLedgerAuthority value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditLedgerAuthority value) => value.Value;

    public static explicit operator BillingCreditLedgerAuthority(string value) => new(value);

    internal class BillingCreditLedgerAuthoritySerializer
        : JsonConverter<BillingCreditLedgerAuthority>
    {
        public override BillingCreditLedgerAuthority Read(
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
            return new BillingCreditLedgerAuthority(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingCreditLedgerAuthority value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingCreditLedgerAuthority ReadAsPropertyName(
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
            return new BillingCreditLedgerAuthority(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingCreditLedgerAuthority value,
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
        public const string SchematicAuthoritative = "schematic_authoritative";

        public const string ExternalMirror = "external_mirror";

        public const string ExternalRated = "external_rated";
    }
}
