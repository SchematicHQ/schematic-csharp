using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditUsageReason.CreditUsageReasonSerializer))]
[Serializable]
public readonly record struct CreditUsageReason : IStringEnum
{
    public static readonly CreditUsageReason LeaseHold = new(Values.LeaseHold);

    public static readonly CreditUsageReason LeaseRelease = new(Values.LeaseRelease);

    public static readonly CreditUsageReason ManualAdjustment = new(Values.ManualAdjustment);

    public static readonly CreditUsageReason Reconciliation = new(Values.Reconciliation);

    public static readonly CreditUsageReason Track = new(Values.Track);

    public CreditUsageReason(string value)
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
    public static CreditUsageReason FromCustom(string value)
    {
        return new CreditUsageReason(value);
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

    public static bool operator ==(CreditUsageReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditUsageReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditUsageReason value) => value.Value;

    public static explicit operator CreditUsageReason(string value) => new(value);

    internal class CreditUsageReasonSerializer : JsonConverter<CreditUsageReason>
    {
        public override CreditUsageReason Read(
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
            return new CreditUsageReason(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditUsageReason value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditUsageReason ReadAsPropertyName(
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
            return new CreditUsageReason(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditUsageReason value,
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
        public const string LeaseHold = "lease_hold";

        public const string LeaseRelease = "lease_release";

        public const string ManualAdjustment = "manual_adjustment";

        public const string Reconciliation = "reconciliation";

        public const string Track = "track";
    }
}
