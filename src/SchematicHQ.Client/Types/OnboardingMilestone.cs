using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(OnboardingMilestone.OnboardingMilestoneSerializer))]
[Serializable]
public readonly record struct OnboardingMilestone : IStringEnum
{
    public static readonly OnboardingMilestone Evaluated = new(Values.Evaluated);

    public static readonly OnboardingMilestone Implemented = new(Values.Implemented);

    public OnboardingMilestone(string value)
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
    public static OnboardingMilestone FromCustom(string value)
    {
        return new OnboardingMilestone(value);
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

    public static bool operator ==(OnboardingMilestone value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OnboardingMilestone value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OnboardingMilestone value) => value.Value;

    public static explicit operator OnboardingMilestone(string value) => new(value);

    internal class OnboardingMilestoneSerializer : JsonConverter<OnboardingMilestone>
    {
        public override OnboardingMilestone Read(
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
            return new OnboardingMilestone(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OnboardingMilestone value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OnboardingMilestone ReadAsPropertyName(
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
            return new OnboardingMilestone(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OnboardingMilestone value,
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
        public const string Evaluated = "evaluated";

        public const string Implemented = "implemented";
    }
}
