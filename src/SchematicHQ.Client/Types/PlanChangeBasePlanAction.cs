using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanChangeBasePlanAction.PlanChangeBasePlanActionSerializer))]
[Serializable]
public readonly record struct PlanChangeBasePlanAction : IStringEnum
{
    public static readonly PlanChangeBasePlanAction Fallback = new(Values.Fallback);

    public static readonly PlanChangeBasePlanAction Initial = new(Values.Initial);

    public static readonly PlanChangeBasePlanAction Trait = new(Values.Trait);

    public static readonly PlanChangeBasePlanAction TrialExpiry = new(Values.TrialExpiry);

    public PlanChangeBasePlanAction(string value)
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
    public static PlanChangeBasePlanAction FromCustom(string value)
    {
        return new PlanChangeBasePlanAction(value);
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

    public static bool operator ==(PlanChangeBasePlanAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeBasePlanAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeBasePlanAction value) => value.Value;

    public static explicit operator PlanChangeBasePlanAction(string value) => new(value);

    internal class PlanChangeBasePlanActionSerializer : JsonConverter<PlanChangeBasePlanAction>
    {
        public override PlanChangeBasePlanAction Read(
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
            return new PlanChangeBasePlanAction(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanChangeBasePlanAction value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanChangeBasePlanAction ReadAsPropertyName(
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
            return new PlanChangeBasePlanAction(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanChangeBasePlanAction value,
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
        public const string Fallback = "fallback";

        public const string Initial = "initial";

        public const string Trait = "trait";

        public const string TrialExpiry = "trial_expiry";
    }
}
