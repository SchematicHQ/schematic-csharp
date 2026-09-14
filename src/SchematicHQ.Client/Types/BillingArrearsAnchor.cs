using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingArrearsAnchor.BillingArrearsAnchorSerializer))]
[Serializable]
public readonly record struct BillingArrearsAnchor : IStringEnum
{
    public static readonly BillingArrearsAnchor BillingPeriodStart = new(Values.BillingPeriodStart);

    public static readonly BillingArrearsAnchor MonthEnd = new(Values.MonthEnd);

    public BillingArrearsAnchor(string value)
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
    public static BillingArrearsAnchor FromCustom(string value)
    {
        return new BillingArrearsAnchor(value);
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

    public static bool operator ==(BillingArrearsAnchor value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingArrearsAnchor value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingArrearsAnchor value) => value.Value;

    public static explicit operator BillingArrearsAnchor(string value) => new(value);

    internal class BillingArrearsAnchorSerializer : JsonConverter<BillingArrearsAnchor>
    {
        public override BillingArrearsAnchor Read(
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
            return new BillingArrearsAnchor(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingArrearsAnchor value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingArrearsAnchor ReadAsPropertyName(
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
            return new BillingArrearsAnchor(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingArrearsAnchor value,
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
        public const string BillingPeriodStart = "billing_period_start";

        public const string MonthEnd = "month_end";
    }
}
