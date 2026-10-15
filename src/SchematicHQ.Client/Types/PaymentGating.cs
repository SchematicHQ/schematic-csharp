using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PaymentGating.PaymentGatingSerializer))]
[Serializable]
public readonly record struct PaymentGating : IStringEnum
{
    public static readonly PaymentGating KeepServing = new(Values.KeepServing);

    public static readonly PaymentGating RevokeAfter = new(Values.RevokeAfter);

    public PaymentGating(string value)
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
    public static PaymentGating FromCustom(string value)
    {
        return new PaymentGating(value);
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

    public static bool operator ==(PaymentGating value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentGating value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentGating value) => value.Value;

    public static explicit operator PaymentGating(string value) => new(value);

    internal class PaymentGatingSerializer : JsonConverter<PaymentGating>
    {
        public override PaymentGating Read(
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
            return new PaymentGating(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PaymentGating value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PaymentGating ReadAsPropertyName(
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
            return new PaymentGating(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PaymentGating value,
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
        public const string KeepServing = "keep_serving";

        public const string RevokeAfter = "revoke_after";
    }
}
