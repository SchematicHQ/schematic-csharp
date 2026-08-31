using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(OnboardingStripeImport.OnboardingStripeImportSerializer))]
[Serializable]
public readonly record struct OnboardingStripeImport : IStringEnum
{
    public static readonly OnboardingStripeImport Complete = new(Values.Complete);

    public static readonly OnboardingStripeImport NotStarted = new(Values.NotStarted);

    public static readonly OnboardingStripeImport Running = new(Values.Running);

    public OnboardingStripeImport(string value)
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
    public static OnboardingStripeImport FromCustom(string value)
    {
        return new OnboardingStripeImport(value);
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

    public static bool operator ==(OnboardingStripeImport value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OnboardingStripeImport value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OnboardingStripeImport value) => value.Value;

    public static explicit operator OnboardingStripeImport(string value) => new(value);

    internal class OnboardingStripeImportSerializer : JsonConverter<OnboardingStripeImport>
    {
        public override OnboardingStripeImport Read(
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
            return new OnboardingStripeImport(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OnboardingStripeImport value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OnboardingStripeImport ReadAsPropertyName(
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
            return new OnboardingStripeImport(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OnboardingStripeImport value,
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
        public const string Complete = "complete";

        public const string NotStarted = "not_started";

        public const string Running = "running";
    }
}
