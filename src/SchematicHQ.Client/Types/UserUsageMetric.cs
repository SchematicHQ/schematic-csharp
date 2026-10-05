using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(UserUsageMetric.UserUsageMetricSerializer))]
[Serializable]
public readonly record struct UserUsageMetric : IStringEnum
{
    public static readonly UserUsageMetric Credits = new(Values.Credits);

    public static readonly UserUsageMetric Feature = new(Values.Feature);

    public UserUsageMetric(string value)
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
    public static UserUsageMetric FromCustom(string value)
    {
        return new UserUsageMetric(value);
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

    public static bool operator ==(UserUsageMetric value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UserUsageMetric value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UserUsageMetric value) => value.Value;

    public static explicit operator UserUsageMetric(string value) => new(value);

    internal class UserUsageMetricSerializer : JsonConverter<UserUsageMetric>
    {
        public override UserUsageMetric Read(
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
            return new UserUsageMetric(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            UserUsageMetric value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override UserUsageMetric ReadAsPropertyName(
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
            return new UserUsageMetric(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            UserUsageMetric value,
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
        public const string Credits = "credits";

        public const string Feature = "feature";
    }
}
