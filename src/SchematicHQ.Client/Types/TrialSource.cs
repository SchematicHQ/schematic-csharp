using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(TrialSource.TrialSourceSerializer))]
[Serializable]
public readonly record struct TrialSource : IStringEnum
{
    public static readonly TrialSource Api = new(Values.Api);

    public static readonly TrialSource CustomPlan = new(Values.CustomPlan);

    public static readonly TrialSource Subscription = new(Values.Subscription);

    public TrialSource(string value)
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
    public static TrialSource FromCustom(string value)
    {
        return new TrialSource(value);
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

    public static bool operator ==(TrialSource value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TrialSource value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TrialSource value) => value.Value;

    public static explicit operator TrialSource(string value) => new(value);

    internal class TrialSourceSerializer : JsonConverter<TrialSource>
    {
        public override TrialSource Read(
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
            return new TrialSource(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TrialSource value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TrialSource ReadAsPropertyName(
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
            return new TrialSource(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TrialSource value,
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
        public const string Api = "api";

        public const string CustomPlan = "custom_plan";

        public const string Subscription = "subscription";
    }
}
