using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(OnboardingTrack.OnboardingTrackSerializer))]
[Serializable]
public readonly record struct OnboardingTrack : IStringEnum
{
    public static readonly OnboardingTrack Catalog = new(Values.Catalog);

    public static readonly OnboardingTrack SinglePlan = new(Values.SinglePlan);

    public OnboardingTrack(string value)
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
    public static OnboardingTrack FromCustom(string value)
    {
        return new OnboardingTrack(value);
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

    public static bool operator ==(OnboardingTrack value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OnboardingTrack value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OnboardingTrack value) => value.Value;

    public static explicit operator OnboardingTrack(string value) => new(value);

    internal class OnboardingTrackSerializer : JsonConverter<OnboardingTrack>
    {
        public override OnboardingTrack Read(
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
            return new OnboardingTrack(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OnboardingTrack value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OnboardingTrack ReadAsPropertyName(
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
            return new OnboardingTrack(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OnboardingTrack value,
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
        public const string Catalog = "catalog";

        public const string SinglePlan = "single_plan";
    }
}
