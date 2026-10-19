using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(SubscriptionChangeAction.SubscriptionChangeActionSerializer))]
[Serializable]
public readonly record struct SubscriptionChangeAction : IStringEnum
{
    public static readonly SubscriptionChangeAction Add = new(Values.Add);

    public static readonly SubscriptionChangeAction Cancel = new(Values.Cancel);

    public static readonly SubscriptionChangeAction Remove = new(Values.Remove);

    public static readonly SubscriptionChangeAction Replace = new(Values.Replace);

    public SubscriptionChangeAction(string value)
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
    public static SubscriptionChangeAction FromCustom(string value)
    {
        return new SubscriptionChangeAction(value);
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

    public static bool operator ==(SubscriptionChangeAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionChangeAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionChangeAction value) => value.Value;

    public static explicit operator SubscriptionChangeAction(string value) => new(value);

    internal class SubscriptionChangeActionSerializer : JsonConverter<SubscriptionChangeAction>
    {
        public override SubscriptionChangeAction Read(
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
            return new SubscriptionChangeAction(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SubscriptionChangeAction value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SubscriptionChangeAction ReadAsPropertyName(
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
            return new SubscriptionChangeAction(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SubscriptionChangeAction value,
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
        public const string Add = "add";

        public const string Cancel = "cancel";

        public const string Remove = "remove";

        public const string Replace = "replace";
    }
}
