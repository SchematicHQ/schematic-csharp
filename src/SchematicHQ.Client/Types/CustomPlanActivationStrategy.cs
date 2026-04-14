using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CustomPlanActivationStrategy.CustomPlanActivationStrategySerializer))]
[Serializable]
public readonly record struct CustomPlanActivationStrategy : IStringEnum
{
    public static readonly CustomPlanActivationStrategy OnPayment = new(Values.OnPayment);

    public static readonly CustomPlanActivationStrategy OnPublish = new(Values.OnPublish);

    public CustomPlanActivationStrategy(string value)
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
    public static CustomPlanActivationStrategy FromCustom(string value)
    {
        return new CustomPlanActivationStrategy(value);
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

    public static bool operator ==(CustomPlanActivationStrategy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CustomPlanActivationStrategy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CustomPlanActivationStrategy value) => value.Value;

    public static explicit operator CustomPlanActivationStrategy(string value) => new(value);

    internal class CustomPlanActivationStrategySerializer
        : JsonConverter<CustomPlanActivationStrategy>
    {
        public override CustomPlanActivationStrategy Read(
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
            return new CustomPlanActivationStrategy(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CustomPlanActivationStrategy value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CustomPlanActivationStrategy ReadAsPropertyName(
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
            return new CustomPlanActivationStrategy(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CustomPlanActivationStrategy value,
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
        public const string OnPayment = "on_payment";

        public const string OnPublish = "on_publish";
    }
}
