using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(ConditionType.ConditionTypeSerializer))]
[Serializable]
public readonly record struct ConditionType : IStringEnum
{
    public static readonly ConditionType BasePlan = new(Values.BasePlan);

    public static readonly ConditionType BillingProduct = new(Values.BillingProduct);

    public static readonly ConditionType Company = new(Values.Company);

    public static readonly ConditionType Credit = new(Values.Credit);

    public static readonly ConditionType Metric = new(Values.Metric);

    public static readonly ConditionType Plan = new(Values.Plan);

    public static readonly ConditionType PlanVersion = new(Values.PlanVersion);

    public static readonly ConditionType Trait = new(Values.Trait);

    public static readonly ConditionType User = new(Values.User);

    public ConditionType(string value)
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
    public static ConditionType FromCustom(string value)
    {
        return new ConditionType(value);
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

    public static bool operator ==(ConditionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ConditionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ConditionType value) => value.Value;

    public static explicit operator ConditionType(string value) => new(value);

    internal class ConditionTypeSerializer : JsonConverter<ConditionType>
    {
        public override ConditionType Read(
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
            return new ConditionType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ConditionType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ConditionType ReadAsPropertyName(
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
            return new ConditionType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ConditionType value,
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
        public const string BasePlan = "base_plan";

        public const string BillingProduct = "billing_product";

        public const string Company = "company";

        public const string Credit = "credit";

        public const string Metric = "metric";

        public const string Plan = "plan";

        public const string PlanVersion = "plan_version";

        public const string Trait = "trait";

        public const string User = "user";
    }
}
