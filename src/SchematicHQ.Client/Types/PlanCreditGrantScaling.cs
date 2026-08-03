using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanCreditGrantScaling.PlanCreditGrantScalingSerializer))]
[Serializable]
public readonly record struct PlanCreditGrantScaling : IStringEnum
{
    public static readonly PlanCreditGrantScaling Fixed = new(Values.Fixed);

    public static readonly PlanCreditGrantScaling PerLicense = new(Values.PerLicense);

    public PlanCreditGrantScaling(string value)
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
    public static PlanCreditGrantScaling FromCustom(string value)
    {
        return new PlanCreditGrantScaling(value);
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

    public static bool operator ==(PlanCreditGrantScaling value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanCreditGrantScaling value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanCreditGrantScaling value) => value.Value;

    public static explicit operator PlanCreditGrantScaling(string value) => new(value);

    internal class PlanCreditGrantScalingSerializer : JsonConverter<PlanCreditGrantScaling>
    {
        public override PlanCreditGrantScaling Read(
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
            return new PlanCreditGrantScaling(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanCreditGrantScaling value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanCreditGrantScaling ReadAsPropertyName(
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
            return new PlanCreditGrantScaling(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanCreditGrantScaling value,
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
        public const string Fixed = "fixed";

        public const string PerLicense = "per_license";
    }
}
