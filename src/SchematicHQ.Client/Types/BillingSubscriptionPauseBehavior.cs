using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(BillingSubscriptionPauseBehavior.BillingSubscriptionPauseBehaviorSerializer))]
[Serializable]
public readonly record struct BillingSubscriptionPauseBehavior : IStringEnum
{
    public static readonly BillingSubscriptionPauseBehavior KeepAsDraft = new(Values.KeepAsDraft);

    public static readonly BillingSubscriptionPauseBehavior MarkUncollectible = new(
        Values.MarkUncollectible
    );

    public static readonly BillingSubscriptionPauseBehavior Void = new(Values.Void);

    public BillingSubscriptionPauseBehavior(string value)
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
    public static BillingSubscriptionPauseBehavior FromCustom(string value)
    {
        return new BillingSubscriptionPauseBehavior(value);
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

    public static bool operator ==(BillingSubscriptionPauseBehavior value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingSubscriptionPauseBehavior value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingSubscriptionPauseBehavior value) => value.Value;

    public static explicit operator BillingSubscriptionPauseBehavior(string value) => new(value);

    internal class BillingSubscriptionPauseBehaviorSerializer
        : JsonConverter<BillingSubscriptionPauseBehavior>
    {
        public override BillingSubscriptionPauseBehavior Read(
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
            return new BillingSubscriptionPauseBehavior(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BillingSubscriptionPauseBehavior value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BillingSubscriptionPauseBehavior ReadAsPropertyName(
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
            return new BillingSubscriptionPauseBehavior(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BillingSubscriptionPauseBehavior value,
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
        public const string KeepAsDraft = "keep_as_draft";

        public const string MarkUncollectible = "mark_uncollectible";

        public const string Void = "void";
    }
}
