using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<WebhookRequestType>))]
[Serializable]
public readonly record struct WebhookRequestType : IStringEnum
{
    public static readonly WebhookRequestType SubscriptionTrialEnded = new(
        Values.SubscriptionTrialEnded
    );

    public static readonly WebhookRequestType CompanyCreated = new(Values.CompanyCreated);

    public static readonly WebhookRequestType CompanyDeleted = new(Values.CompanyDeleted);

    public static readonly WebhookRequestType CompanyOverrideCreated = new(
        Values.CompanyOverrideCreated
    );

    public static readonly WebhookRequestType CompanyOverrideDeleted = new(
        Values.CompanyOverrideDeleted
    );

    public static readonly WebhookRequestType CompanyOverrideExpired = new(
        Values.CompanyOverrideExpired
    );

    public static readonly WebhookRequestType CompanyOverrideUpdated = new(
        Values.CompanyOverrideUpdated
    );

    public static readonly WebhookRequestType CompanyPlanChanged = new(Values.CompanyPlanChanged);

    public static readonly WebhookRequestType CompanyUpdated = new(Values.CompanyUpdated);

    public static readonly WebhookRequestType CreditLimitReached = new(Values.CreditLimitReached);

    public static readonly WebhookRequestType CreditLimitWarning = new(Values.CreditLimitWarning);

    public static readonly WebhookRequestType EntitlementLimitReached = new(
        Values.EntitlementLimitReached
    );

    public static readonly WebhookRequestType EntitlementLimitWarning = new(
        Values.EntitlementLimitWarning
    );

    public static readonly WebhookRequestType EntitlementSoftLimitReached = new(
        Values.EntitlementSoftLimitReached
    );

    public static readonly WebhookRequestType EntitlementSoftLimitWarning = new(
        Values.EntitlementSoftLimitWarning
    );

    public static readonly WebhookRequestType EntitlementTierLimitReached = new(
        Values.EntitlementTierLimitReached
    );

    public static readonly WebhookRequestType EntitlementTierLimitWarning = new(
        Values.EntitlementTierLimitWarning
    );

    public static readonly WebhookRequestType FeatureCreated = new(Values.FeatureCreated);

    public static readonly WebhookRequestType FeatureDeleted = new(Values.FeatureDeleted);

    public static readonly WebhookRequestType FeatureUpdated = new(Values.FeatureUpdated);

    public static readonly WebhookRequestType FlagCreated = new(Values.FlagCreated);

    public static readonly WebhookRequestType FlagDeleted = new(Values.FlagDeleted);

    public static readonly WebhookRequestType FlagRulesUpdated = new(Values.FlagRulesUpdated);

    public static readonly WebhookRequestType FlagUpdated = new(Values.FlagUpdated);

    public static readonly WebhookRequestType PlanCreated = new(Values.PlanCreated);

    public static readonly WebhookRequestType PlanDeleted = new(Values.PlanDeleted);

    public static readonly WebhookRequestType PlanEntitlementCreated = new(
        Values.PlanEntitlementCreated
    );

    public static readonly WebhookRequestType PlanEntitlementDeleted = new(
        Values.PlanEntitlementDeleted
    );

    public static readonly WebhookRequestType PlanEntitlementUpdated = new(
        Values.PlanEntitlementUpdated
    );

    public static readonly WebhookRequestType PlanUpdated = new(Values.PlanUpdated);

    public static readonly WebhookRequestType RuleDeleted = new(Values.RuleDeleted);

    public static readonly WebhookRequestType TestSend = new(Values.TestSend);

    public static readonly WebhookRequestType UserCreated = new(Values.UserCreated);

    public static readonly WebhookRequestType UserDeleted = new(Values.UserDeleted);

    public static readonly WebhookRequestType UserUpdated = new(Values.UserUpdated);

    public static readonly WebhookRequestType AutoTopupHardFailure = new(
        Values.AutoTopupHardFailure
    );

    public static readonly WebhookRequestType AutoTopupRetryExceeded = new(
        Values.AutoTopupRetryExceeded
    );

    public WebhookRequestType(string value)
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
    public static WebhookRequestType FromCustom(string value)
    {
        return new WebhookRequestType(value);
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

    public static bool operator ==(WebhookRequestType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(WebhookRequestType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(WebhookRequestType value) => value.Value;

    public static explicit operator WebhookRequestType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string SubscriptionTrialEnded = "subscription.trial.ended";

        public const string CompanyCreated = "company.created";

        public const string CompanyDeleted = "company.deleted";

        public const string CompanyOverrideCreated = "company.override.created";

        public const string CompanyOverrideDeleted = "company.override.deleted";

        public const string CompanyOverrideExpired = "company.override.expired";

        public const string CompanyOverrideUpdated = "company.override.updated";

        public const string CompanyPlanChanged = "company.plan_changed";

        public const string CompanyUpdated = "company.updated";

        public const string CreditLimitReached = "credit.limit.reached";

        public const string CreditLimitWarning = "credit.limit.warning";

        public const string EntitlementLimitReached = "entitlement.limit.reached";

        public const string EntitlementLimitWarning = "entitlement.limit.warning";

        public const string EntitlementSoftLimitReached = "entitlement.soft_limit.reached";

        public const string EntitlementSoftLimitWarning = "entitlement.soft_limit.warning";

        public const string EntitlementTierLimitReached = "entitlement.tier_limit.reached";

        public const string EntitlementTierLimitWarning = "entitlement.tier_limit.warning";

        public const string FeatureCreated = "feature.created";

        public const string FeatureDeleted = "feature.deleted";

        public const string FeatureUpdated = "feature.updated";

        public const string FlagCreated = "flag.created";

        public const string FlagDeleted = "flag.deleted";

        public const string FlagRulesUpdated = "flag_rules.updated";

        public const string FlagUpdated = "flag.updated";

        public const string PlanCreated = "plan.created";

        public const string PlanDeleted = "plan.deleted";

        public const string PlanEntitlementCreated = "plan.entitlement.created";

        public const string PlanEntitlementDeleted = "plan.entitlement.deleted";

        public const string PlanEntitlementUpdated = "plan.entitlement.updated";

        public const string PlanUpdated = "plan.updated";

        public const string RuleDeleted = "rule.deleted";

        public const string TestSend = "test.send";

        public const string UserCreated = "user.created";

        public const string UserDeleted = "user.deleted";

        public const string UserUpdated = "user.updated";

        public const string AutoTopupHardFailure = "auto.topup.hard.failure";

        public const string AutoTopupRetryExceeded = "auto.topup.retry.exceeded";
    }
}
