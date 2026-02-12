using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineConditionConditionType>))]
[Serializable]
public readonly record struct RulesengineConditionConditionType : IStringEnum
{
    public static readonly RulesengineConditionConditionType BasePlan = new(Values.BasePlan);

    public static readonly RulesengineConditionConditionType BillingProduct = new(
        Values.BillingProduct
    );

    public static readonly RulesengineConditionConditionType Company = new(Values.Company);

    public static readonly RulesengineConditionConditionType Credit = new(Values.Credit);

    public static readonly RulesengineConditionConditionType Metric = new(Values.Metric);

    public static readonly RulesengineConditionConditionType Plan = new(Values.Plan);

    public static readonly RulesengineConditionConditionType PlanVersion = new(Values.PlanVersion);

    public static readonly RulesengineConditionConditionType Trait = new(Values.Trait);

    public static readonly RulesengineConditionConditionType User = new(Values.User);

    public RulesengineConditionConditionType(string value)
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
    public static RulesengineConditionConditionType FromCustom(string value)
    {
        return new RulesengineConditionConditionType(value);
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

    public static bool operator ==(RulesengineConditionConditionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineConditionConditionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineConditionConditionType value) => value.Value;

    public static explicit operator RulesengineConditionConditionType(string value) => new(value);

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
