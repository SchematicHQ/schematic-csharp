using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(OnboardingRequirementStatus.OnboardingRequirementStatusSerializer))]
[Serializable]
public readonly record struct OnboardingRequirementStatus : IStringEnum
{
    public static readonly OnboardingRequirementStatus Available = new(Values.Available);

    public static readonly OnboardingRequirementStatus Blocked = new(Values.Blocked);

    public static readonly OnboardingRequirementStatus Complete = new(Values.Complete);

    public OnboardingRequirementStatus(string value)
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
    public static OnboardingRequirementStatus FromCustom(string value)
    {
        return new OnboardingRequirementStatus(value);
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

    public static bool operator ==(OnboardingRequirementStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OnboardingRequirementStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OnboardingRequirementStatus value) => value.Value;

    public static explicit operator OnboardingRequirementStatus(string value) => new(value);

    internal class OnboardingRequirementStatusSerializer
        : JsonConverter<OnboardingRequirementStatus>
    {
        public override OnboardingRequirementStatus Read(
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
            return new OnboardingRequirementStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OnboardingRequirementStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OnboardingRequirementStatus ReadAsPropertyName(
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
            return new OnboardingRequirementStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OnboardingRequirementStatus value,
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
        public const string Available = "available";

        public const string Blocked = "blocked";

        public const string Complete = "complete";
    }
}
