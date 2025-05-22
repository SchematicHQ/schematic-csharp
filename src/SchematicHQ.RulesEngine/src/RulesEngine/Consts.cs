namespace SchematicHQ.RulesEngine
{
  public enum ConditionType
  {
    BasePlan,
    BillingProduct,
    Company,
    CrmProduct,
    Metric,
    Plan,
    Trait,
    User
  }

  public enum EntityType
  {
    Company,
    User
  }

  public enum MetricPeriod
  {
    AllTime,
    CurrentDay,
    CurrentMonth,
    CurrentWeek
  }

  public enum MetricPeriodMonthReset
  {
    First,
    Billing
  }

  public enum RuleType
  {
    GlobalOverride,
    CompanyOverride,
    CompanyOverrideUsageExceeded,
    PlanEntitlement,
    PlanEntitlementUsageExceeded,
    Standard,
    Default,
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

  public enum RulePrioritizationMethod
  {
    None,
    Priority,
    Optimistic
  }
}