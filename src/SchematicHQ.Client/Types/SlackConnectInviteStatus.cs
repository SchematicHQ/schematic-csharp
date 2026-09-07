using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(SlackConnectInviteStatus.SlackConnectInviteStatusSerializer))]
[Serializable]
public readonly record struct SlackConnectInviteStatus : IStringEnum
{
    public static readonly SlackConnectInviteStatus Accepted = new(Values.Accepted);

    public static readonly SlackConnectInviteStatus Declined = new(Values.Declined);

    public static readonly SlackConnectInviteStatus Dismissed = new(Values.Dismissed);

    public static readonly SlackConnectInviteStatus Expired = new(Values.Expired);

    public static readonly SlackConnectInviteStatus Pending = new(Values.Pending);

    public SlackConnectInviteStatus(string value)
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
    public static SlackConnectInviteStatus FromCustom(string value)
    {
        return new SlackConnectInviteStatus(value);
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

    public static bool operator ==(SlackConnectInviteStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SlackConnectInviteStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SlackConnectInviteStatus value) => value.Value;

    public static explicit operator SlackConnectInviteStatus(string value) => new(value);

    internal class SlackConnectInviteStatusSerializer : JsonConverter<SlackConnectInviteStatus>
    {
        public override SlackConnectInviteStatus Read(
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
            return new SlackConnectInviteStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SlackConnectInviteStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SlackConnectInviteStatus ReadAsPropertyName(
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
            return new SlackConnectInviteStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SlackConnectInviteStatus value,
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
        public const string Accepted = "accepted";

        public const string Declined = "declined";

        public const string Dismissed = "dismissed";

        public const string Expired = "expired";

        public const string Pending = "pending";
    }
}
