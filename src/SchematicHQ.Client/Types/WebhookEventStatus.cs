using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(WebhookEventStatus.WebhookEventStatusSerializer))]
[Serializable]
public readonly record struct WebhookEventStatus : IStringEnum
{
    public static readonly WebhookEventStatus Failure = new(Values.Failure);

    public static readonly WebhookEventStatus Pending = new(Values.Pending);

    public static readonly WebhookEventStatus Success = new(Values.Success);

    public WebhookEventStatus(string value)
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
    public static WebhookEventStatus FromCustom(string value)
    {
        return new WebhookEventStatus(value);
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

    public static bool operator ==(WebhookEventStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(WebhookEventStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(WebhookEventStatus value) => value.Value;

    public static explicit operator WebhookEventStatus(string value) => new(value);

    internal class WebhookEventStatusSerializer : JsonConverter<WebhookEventStatus>
    {
        public override WebhookEventStatus Read(
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
            return new WebhookEventStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            WebhookEventStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override WebhookEventStatus ReadAsPropertyName(
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
            return new WebhookEventStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            WebhookEventStatus value,
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
        public const string Failure = "failure";

        public const string Pending = "pending";

        public const string Success = "success";
    }
}
