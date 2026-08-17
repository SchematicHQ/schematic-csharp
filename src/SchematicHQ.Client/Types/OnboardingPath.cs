using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(OnboardingPath.OnboardingPathSerializer))]
[Serializable]
public readonly record struct OnboardingPath : IStringEnum
{
    public static readonly OnboardingPath Agent = new(Values.Agent);

    public static readonly OnboardingPath App = new(Values.App);

    public OnboardingPath(string value)
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
    public static OnboardingPath FromCustom(string value)
    {
        return new OnboardingPath(value);
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

    public static bool operator ==(OnboardingPath value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OnboardingPath value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OnboardingPath value) => value.Value;

    public static explicit operator OnboardingPath(string value) => new(value);

    internal class OnboardingPathSerializer : JsonConverter<OnboardingPath>
    {
        public override OnboardingPath Read(
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
            return new OnboardingPath(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OnboardingPath value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OnboardingPath ReadAsPropertyName(
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
            return new OnboardingPath(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OnboardingPath value,
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
        public const string Agent = "agent";

        public const string App = "app";
    }
}
