using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingArrearsCadence.BillingArrearsCadenceSerializer))]
[Serializable]
public readonly record struct BillingArrearsCadence : IStringEnum
{
    public static readonly BillingArrearsCadence EndOfBillingPeriod = new(
        Values.EndOfBillingPeriod
    );

    public static readonly BillingArrearsCadence Monthly = new(Values.Monthly);

    public BillingArrearsCadence(string value)
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
    public static BillingArrearsCadence FromCustom(string value)
    {
        return new BillingArrearsCadence(value);
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

    public static bool operator ==(BillingArrearsCadence value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingArrearsCadence value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingArrearsCadence value) => value.Value;

    public static explicit operator BillingArrearsCadence(string value) => new(value);

    internal class BillingArrearsCadenceSerializer : JsonConverter<BillingArrearsCadence>
    {
        public override BillingArrearsCadence Read(
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
            return new BillingArrearsCadence(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingArrearsCadence value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingArrearsCadence ReadAsPropertyName(
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
            return new BillingArrearsCadence(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingArrearsCadence value,
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
        public const string EndOfBillingPeriod = "end_of_billing_period";

        public const string Monthly = "monthly";
    }
}
