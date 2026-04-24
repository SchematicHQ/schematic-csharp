using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SchematicHQ.Client.RulesEngine
{
  public static class RulesengineRuleTypeExtensions
  {
    public static string DisplayName(this RulesengineRuleType ruleType)
    {
      return ruleType.Value.Replace("_", " ");
    }

    public static bool IsEntitlement(this RulesengineRuleType ruleType)
    {
      return ruleType == RulesengineRuleType.PlanEntitlement.Value ||
             ruleType == RulesengineRuleType.PlanEntitlementUsageExceeded.Value ||
             ruleType == RulesengineRuleType.CompanyOverride.Value ||
             ruleType == RulesengineRuleType.CompanyOverrideUsageExceeded.Value;
    }

    public static RulePrioritizationMethod PrioritizationMethod(this RulesengineRuleType ruleType)
    {
      if (ruleType == RulesengineRuleType.Standard.Value)
      {
        return RulePrioritizationMethod.Priority;
      }

      if (ruleType == RulesengineRuleType.CompanyOverride.Value ||
          ruleType == RulesengineRuleType.PlanEntitlement.Value ||
          ruleType == RulesengineRuleType.CompanyOverrideUsageExceeded.Value ||
          ruleType == RulesengineRuleType.PlanEntitlementUsageExceeded.Value)
      {
        return RulePrioritizationMethod.Optimistic;
      }

      return RulePrioritizationMethod.None;
    }

    public static List<RulesengineRuleType> RuleTypePriority = new List<RulesengineRuleType>
        {
            RulesengineRuleType.GlobalOverride,
            RulesengineRuleType.CompanyOverride,
            RulesengineRuleType.PlanEntitlement,
            RulesengineRuleType.CompanyOverrideUsageExceeded,
            RulesengineRuleType.PlanEntitlementUsageExceeded,
            RulesengineRuleType.Standard,
            RulesengineRuleType.Default
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
