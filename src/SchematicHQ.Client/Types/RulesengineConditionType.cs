using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RulesengineConditionType.RulesengineConditionTypeSerializer))]
[Serializable]
public readonly record struct RulesengineConditionType : IStringEnum
{
    public static readonly RulesengineConditionType BasePlan = new(Values.BasePlan);

    public static readonly RulesengineConditionType BillingProduct = new(Values.BillingProduct);

    public static readonly RulesengineConditionType Company = new(Values.Company);

    public static readonly RulesengineConditionType Credit = new(Values.Credit);

    public static readonly RulesengineConditionType Metric = new(Values.Metric);

    public static readonly RulesengineConditionType Plan = new(Values.Plan);

    public static readonly RulesengineConditionType PlanVersion = new(Values.PlanVersion);

    public static readonly RulesengineConditionType Trait = new(Values.Trait);

    public static readonly RulesengineConditionType User = new(Values.User);

    public RulesengineConditionType(string value)
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
    public static RulesengineConditionType FromCustom(string value)
    {
        return new RulesengineConditionType(value);
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

    public static bool operator ==(RulesengineConditionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineConditionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineConditionType value) => value.Value;

    public static explicit operator RulesengineConditionType(string value) => new(value);

    internal class RulesengineConditionTypeSerializer : JsonConverter<RulesengineConditionType>
    {
        public override RulesengineConditionType Read(
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
            return new RulesengineConditionType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineConditionType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineConditionType ReadAsPropertyName(
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
            return new RulesengineConditionType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineConditionType value,
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
