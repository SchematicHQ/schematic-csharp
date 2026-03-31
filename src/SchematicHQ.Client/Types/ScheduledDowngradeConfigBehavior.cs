using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(ScheduledDowngradeConfigBehavior.ScheduledDowngradeConfigBehaviorSerializer))]
[Serializable]
public readonly record struct ScheduledDowngradeConfigBehavior : IStringEnum
{
    public static readonly ScheduledDowngradeConfigBehavior EndOfBillingPeriod = new(
        Values.EndOfBillingPeriod
    );

    public static readonly ScheduledDowngradeConfigBehavior None = new(Values.None);

    public ScheduledDowngradeConfigBehavior(string value)
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
    public static ScheduledDowngradeConfigBehavior FromCustom(string value)
    {
        return new ScheduledDowngradeConfigBehavior(value);
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

    public static bool operator ==(ScheduledDowngradeConfigBehavior value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ScheduledDowngradeConfigBehavior value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ScheduledDowngradeConfigBehavior value) => value.Value;

    public static explicit operator ScheduledDowngradeConfigBehavior(string value) => new(value);

    internal class ScheduledDowngradeConfigBehaviorSerializer
        : JsonConverter<ScheduledDowngradeConfigBehavior>
    {
        public override ScheduledDowngradeConfigBehavior Read(
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
            return new ScheduledDowngradeConfigBehavior(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ScheduledDowngradeConfigBehavior value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ScheduledDowngradeConfigBehavior ReadAsPropertyName(
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
            return new ScheduledDowngradeConfigBehavior(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ScheduledDowngradeConfigBehavior value,
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
        public const string EndOfBillingPeriod = "end_of_billing_period";

        public const string None = "none";
    }
}
