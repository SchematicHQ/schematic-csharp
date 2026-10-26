using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CheckoutChargeTiming.CheckoutChargeTimingSerializer))]
[Serializable]
public readonly record struct CheckoutChargeTiming : IStringEnum
{
    public static readonly CheckoutChargeTiming AtRenewal = new(Values.AtRenewal);

    public static readonly CheckoutChargeTiming None = new(Values.None);

    public static readonly CheckoutChargeTiming Now = new(Values.Now);

    public CheckoutChargeTiming(string value)
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
    public static CheckoutChargeTiming FromCustom(string value)
    {
        return new CheckoutChargeTiming(value);
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

    public static bool operator ==(CheckoutChargeTiming value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CheckoutChargeTiming value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CheckoutChargeTiming value) => value.Value;

    public static explicit operator CheckoutChargeTiming(string value) => new(value);

    internal class CheckoutChargeTimingSerializer : JsonConverter<CheckoutChargeTiming>
    {
        public override CheckoutChargeTiming Read(
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
            return new CheckoutChargeTiming(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CheckoutChargeTiming value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CheckoutChargeTiming ReadAsPropertyName(
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
            return new CheckoutChargeTiming(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CheckoutChargeTiming value,
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
        public const string AtRenewal = "at_renewal";

        public const string None = "none";

        public const string Now = "now";
    }
}
