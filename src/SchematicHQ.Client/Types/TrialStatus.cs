using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(TrialStatus.TrialStatusSerializer))]
[Serializable]
public readonly record struct TrialStatus : IStringEnum
{
    public static readonly TrialStatus Active = new(Values.Active);

    public static readonly TrialStatus Converted = new(Values.Converted);

    public static readonly TrialStatus Expired = new(Values.Expired);

    public TrialStatus(string value)
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
    public static TrialStatus FromCustom(string value)
    {
        return new TrialStatus(value);
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

    public static bool operator ==(TrialStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TrialStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TrialStatus value) => value.Value;

    public static explicit operator TrialStatus(string value) => new(value);

    internal class TrialStatusSerializer : JsonConverter<TrialStatus>
    {
        public override TrialStatus Read(
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
            return new TrialStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TrialStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TrialStatus ReadAsPropertyName(
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
            return new TrialStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TrialStatus value,
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
        public const string Active = "active";

        public const string Converted = "converted";

        public const string Expired = "expired";
    }
}
