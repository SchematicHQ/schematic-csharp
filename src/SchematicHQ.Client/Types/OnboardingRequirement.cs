using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(OnboardingRequirement.OnboardingRequirementSerializer))]
[Serializable]
public readonly record struct OnboardingRequirement : IStringEnum
{
    public static readonly OnboardingRequirement ConnectAgent = new(Values.ConnectAgent);

    public static readonly OnboardingRequirement ConnectBilling = new(Values.ConnectBilling);

    public static readonly OnboardingRequirement CreateApiKey = new(Values.CreateApiKey);

    public static readonly OnboardingRequirement FirstFlagCheck = new(Values.FirstFlagCheck);

    public static readonly OnboardingRequirement ImportCompanies = new(Values.ImportCompanies);

    public static readonly OnboardingRequirement ModelPackaging = new(Values.ModelPackaging);

    public static readonly OnboardingRequirement SendEvents = new(Values.SendEvents);

    public OnboardingRequirement(string value)
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
    public static OnboardingRequirement FromCustom(string value)
    {
        return new OnboardingRequirement(value);
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

    public static bool operator ==(OnboardingRequirement value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OnboardingRequirement value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OnboardingRequirement value) => value.Value;

    public static explicit operator OnboardingRequirement(string value) => new(value);

    internal class OnboardingRequirementSerializer : JsonConverter<OnboardingRequirement>
    {
        public override OnboardingRequirement Read(
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
            return new OnboardingRequirement(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OnboardingRequirement value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OnboardingRequirement ReadAsPropertyName(
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
            return new OnboardingRequirement(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OnboardingRequirement value,
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
        public const string ConnectAgent = "connect_agent";

        public const string ConnectBilling = "connect_billing";

        public const string CreateApiKey = "create_api_key";

        public const string FirstFlagCheck = "first_flag_check";

        public const string ImportCompanies = "import_companies";

        public const string ModelPackaging = "model_packaging";

        public const string SendEvents = "send_events";
    }
}
