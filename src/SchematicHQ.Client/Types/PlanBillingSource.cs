using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanBillingSource.PlanBillingSourceSerializer))]
[Serializable]
public readonly record struct PlanBillingSource : IStringEnum
{
    public static readonly PlanBillingSource CustomPlan = new(Values.CustomPlan);

    public static readonly PlanBillingSource ManagePlan = new(Values.ManagePlan);

    public PlanBillingSource(string value)
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
    public static PlanBillingSource FromCustom(string value)
    {
        return new PlanBillingSource(value);
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

    public static bool operator ==(PlanBillingSource value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanBillingSource value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanBillingSource value) => value.Value;

    public static explicit operator PlanBillingSource(string value) => new(value);

    internal class PlanBillingSourceSerializer : JsonConverter<PlanBillingSource>
    {
        public override PlanBillingSource Read(
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
            return new PlanBillingSource(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanBillingSource value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanBillingSource ReadAsPropertyName(
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
            return new PlanBillingSource(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanBillingSource value,
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
        public const string CustomPlan = "custom_plan";

        public const string ManagePlan = "manage_plan";
    }
}
