using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CheckoutProblemSource.CheckoutProblemSourceSerializer))]
[Serializable]
public readonly record struct CheckoutProblemSource : IStringEnum
{
    public static readonly CheckoutProblemSource Provider = new(Values.Provider);

    public static readonly CheckoutProblemSource Requirement = new(Values.Requirement);

    public static readonly CheckoutProblemSource Validation = new(Values.Validation);

    public CheckoutProblemSource(string value)
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
    public static CheckoutProblemSource FromCustom(string value)
    {
        return new CheckoutProblemSource(value);
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

    public static bool operator ==(CheckoutProblemSource value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CheckoutProblemSource value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CheckoutProblemSource value) => value.Value;

    public static explicit operator CheckoutProblemSource(string value) => new(value);

    internal class CheckoutProblemSourceSerializer : JsonConverter<CheckoutProblemSource>
    {
        public override CheckoutProblemSource Read(
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
            return new CheckoutProblemSource(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CheckoutProblemSource value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CheckoutProblemSource ReadAsPropertyName(
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
            return new CheckoutProblemSource(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CheckoutProblemSource value,
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
        public const string Provider = "provider";

        public const string Requirement = "requirement";

        public const string Validation = "validation";
    }
}
