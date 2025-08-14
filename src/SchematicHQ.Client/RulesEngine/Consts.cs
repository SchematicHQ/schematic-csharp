using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SchematicHQ.Client.RulesEngine
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum ConditionType
  {
    [JsonPropertyName("base_plan")]
    BasePlan,
    
    [JsonPropertyName("billing_product")]
    BillingProduct,

    [JsonPropertyName("credit")]
    Credit,
    
    [JsonPropertyName("company")]
    Company,
    
    [JsonPropertyName("crm_product")]
    CrmProduct,
    
    [JsonPropertyName("metric")]
    Metric,
    
    [JsonPropertyName("plan")]
    Plan,
    
    [JsonPropertyName("trait")]
    Trait,
    
    [JsonPropertyName("user")]
    User
  }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum EntityType
  {
    [JsonPropertyName("company")]
    Company,
    
    [JsonPropertyName("user")]
    User
  }

  [JsonConverter(typeof(SchematicHQ.Client.RulesEngine.SnakeCaseEnumConverter<MetricPeriod>))]
    public enum MetricPeriod
  {
    AllTime,
    CurrentDay,
    CurrentMonth,
    CurrentWeek
  }

  [JsonConverter(typeof(SchematicHQ.Client.RulesEngine.SnakeCaseEnumConverter<MetricPeriodMonthReset>))]
  public enum MetricPeriodMonthReset
  {
    [JsonPropertyName("first_of_month")]
    FirstOfMonth,
    
    [JsonPropertyName("billing_cycle")]
    BillingCycle
  }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum RuleType
  {
    [JsonPropertyName("global_override")]
    GlobalOverride,
    
    [JsonPropertyName("company_override")]
    CompanyOverride,
    
    [JsonPropertyName("company_override_usage_exceeded")]
    CompanyOverrideUsageExceeded,
    
    [JsonPropertyName("plan_entitlement")]
    PlanEntitlement,
    
    [JsonPropertyName("plan_entitlement_usage_exceeded")]
    PlanEntitlementUsageExceeded,
    
    [JsonPropertyName("standard")]
    Standard,
    
    [JsonPropertyName("default")]
    Default,
    
    [JsonPropertyName("plan_audience")]
    PlanAudience
  }

  public static class RuleTypeExtensions
  {
    public static string DisplayName(this RuleType ruleType)
    {
      return ruleType.ToString().Replace("_", " ");
    }

    public static bool IsEntitlement(this RuleType ruleType)
    {
      return ruleType == RuleType.PlanEntitlement ||
             ruleType == RuleType.PlanEntitlementUsageExceeded ||
             ruleType == RuleType.CompanyOverride ||
             ruleType == RuleType.CompanyOverrideUsageExceeded;
    }

    public static RulePrioritizationMethod PrioritizationMethod(this RuleType ruleType)
    {
      if (ruleType == RuleType.Standard)
      {
        return RulePrioritizationMethod.Priority;
      }

      if (ruleType == RuleType.CompanyOverride ||
          ruleType == RuleType.PlanEntitlement ||
          ruleType == RuleType.CompanyOverrideUsageExceeded ||
          ruleType == RuleType.PlanEntitlementUsageExceeded)
      {
        return RulePrioritizationMethod.Optimistic;
      }

      return RulePrioritizationMethod.None;
    }

    public static List<RuleType> RuleTypePriority = new List<RuleType>
        {
            RuleType.GlobalOverride,
            RuleType.CompanyOverride,
            RuleType.PlanEntitlement,
            RuleType.CompanyOverrideUsageExceeded,
            RuleType.PlanEntitlementUsageExceeded,
            RuleType.Standard,
            RuleType.Default
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