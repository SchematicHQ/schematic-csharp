using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SchematicHQ.Client.RulesEngine
{
  public static class RulesengineRuleRuleTypeExtensions
  {
    public static string DisplayName(this RulesengineRuleRuleType ruleType)
    {
      return ruleType.Value.Replace("_", " ");
    }

    public static bool IsEntitlement(this RulesengineRuleRuleType ruleType)
    {
      return ruleType == RulesengineRuleRuleType.PlanEntitlement.Value ||
             ruleType == RulesengineRuleRuleType.PlanEntitlementUsageExceeded.Value ||
             ruleType == RulesengineRuleRuleType.CompanyOverride.Value ||
             ruleType == RulesengineRuleRuleType.CompanyOverrideUsageExceeded.Value;
    }

    public static RulePrioritizationMethod PrioritizationMethod(this RulesengineRuleRuleType ruleType)
    {
      if (ruleType == RulesengineRuleRuleType.Standard.Value)
      {
        return RulePrioritizationMethod.Priority;
      }

      if (ruleType == RulesengineRuleRuleType.CompanyOverride.Value ||
          ruleType == RulesengineRuleRuleType.PlanEntitlement.Value ||
          ruleType == RulesengineRuleRuleType.CompanyOverrideUsageExceeded.Value ||
          ruleType == RulesengineRuleRuleType.PlanEntitlementUsageExceeded.Value)
      {
        return RulePrioritizationMethod.Optimistic;
      }

      return RulePrioritizationMethod.None;
    }

    public static List<RulesengineRuleRuleType> RuleTypePriority = new List<RulesengineRuleRuleType>
        {
            RulesengineRuleRuleType.GlobalOverride,
            RulesengineRuleRuleType.CompanyOverride,
            RulesengineRuleRuleType.PlanEntitlement,
            RulesengineRuleRuleType.CompanyOverrideUsageExceeded,
            RulesengineRuleRuleType.PlanEntitlementUsageExceeded,
            RulesengineRuleRuleType.Standard,
            RulesengineRuleRuleType.Default
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
