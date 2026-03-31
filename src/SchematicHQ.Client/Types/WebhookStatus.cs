using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(WebhookStatus.WebhookStatusSerializer))]
[Serializable]
public readonly record struct WebhookStatus : IStringEnum
{
    public static readonly WebhookStatus Active = new(Values.Active);

    public static readonly WebhookStatus Inactive = new(Values.Inactive);

    public WebhookStatus(string value)
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
    public static WebhookStatus FromCustom(string value)
    {
        return new WebhookStatus(value);
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

    public static bool operator ==(WebhookStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(WebhookStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(WebhookStatus value) => value.Value;

    public static explicit operator WebhookStatus(string value) => new(value);

    internal class WebhookStatusSerializer : JsonConverter<WebhookStatus>
    {
        public override WebhookStatus Read(
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
            return new WebhookStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            WebhookStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override WebhookStatus ReadAsPropertyName(
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
            return new WebhookStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            WebhookStatus value,
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

        public const string Inactive = "inactive";
    }
}
