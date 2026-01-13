using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RuleRuleType>))]
[Serializable]
public readonly record struct RuleRuleType : IStringEnum
{
    public static readonly RuleRuleType Default = new(Values.Default);

    public static readonly RuleRuleType GlobalOverride = new(Values.GlobalOverride);

    public static readonly RuleRuleType CompanyOverride = new(Values.CompanyOverride);

    public static readonly RuleRuleType CompanyOverrideUsageExceeded = new(
        Values.CompanyOverrideUsageExceeded
    );

    public static readonly RuleRuleType PlanEntitlement = new(Values.PlanEntitlement);

    public static readonly RuleRuleType PlanEntitlementUsageExceeded = new(
        Values.PlanEntitlementUsageExceeded
    );

    public static readonly RuleRuleType Standard = new(Values.Standard);

    public RuleRuleType(string value)
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
    public static RuleRuleType FromCustom(string value)
    {
        return new RuleRuleType(value);
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

    public static bool operator ==(RuleRuleType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RuleRuleType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RuleRuleType value) => value.Value;

    public static explicit operator RuleRuleType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Default = "default";

        public const string GlobalOverride = "global_override";

        public const string CompanyOverride = "company_override";

        public const string CompanyOverrideUsageExceeded = "company_override_usage_exceeded";

        public const string PlanEntitlement = "plan_entitlement";

        public const string PlanEntitlementUsageExceeded = "plan_entitlement_usage_exceeded";

        public const string Standard = "standard";
    }
}
