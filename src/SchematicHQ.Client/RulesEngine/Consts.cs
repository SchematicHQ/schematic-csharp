using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SchematicHQ.Client.RulesEngine
{
  // Note: All enum types now use the generated types from SchematicHQ.Client namespace
  // (RuleRuleType, ConditionConditionType, ConditionMetricPeriod, etc.)

  public static class RuleRuleTypeExtensions
  {
    public static string DisplayName(this RuleRuleType ruleType)
    {
      return ruleType.Value.Replace("_", " ");
    }

    public static bool IsEntitlement(this RuleRuleType ruleType)
    {
      return ruleType == RuleRuleType.PlanEntitlement.Value ||
             ruleType == RuleRuleType.PlanEntitlementUsageExceeded.Value ||
             ruleType == RuleRuleType.CompanyOverride.Value ||
             ruleType == RuleRuleType.CompanyOverrideUsageExceeded.Value;
    }

    public static RulePrioritizationMethod PrioritizationMethod(this RuleRuleType ruleType)
    {
      if (ruleType == RuleRuleType.Standard.Value)
      {
        return RulePrioritizationMethod.Priority;
      }

      if (ruleType == RuleRuleType.CompanyOverride.Value ||
          ruleType == RuleRuleType.PlanEntitlement.Value ||
          ruleType == RuleRuleType.CompanyOverrideUsageExceeded.Value ||
          ruleType == RuleRuleType.PlanEntitlementUsageExceeded.Value)
      {
        return RulePrioritizationMethod.Optimistic;
      }

      return RulePrioritizationMethod.None;
    }

    public static List<RuleRuleType> RuleTypePriority = new List<RuleRuleType>
        {
            RuleRuleType.GlobalOverride,
            RuleRuleType.CompanyOverride,
            RuleRuleType.PlanEntitlement,
            RuleRuleType.CompanyOverrideUsageExceeded,
            RuleRuleType.PlanEntitlementUsageExceeded,
            RuleRuleType.Standard,
            RuleRuleType.Default
        };
  }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum RulePrioritizationMethod
  {
    [JsonPropertyName("none")]
    None,

    [JsonPropertyName("priority")]
    Priority,

    [JsonPropertyName("optimistic")]
    Optimistic
  }
}