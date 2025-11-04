using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateWebhookRequestBodyRequestTypesItem>))]
[Serializable]
public readonly record struct UpdateWebhookRequestBodyRequestTypesItem : IStringEnum
{
    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyUpdated = new(
        Values.CompanyUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem UserUpdated = new(
        Values.UserUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem PlanUpdated = new(
        Values.PlanUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem PlanEntitlementUpdated = new(
        Values.PlanEntitlementUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyOverrideUpdated = new(
        Values.CompanyOverrideUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FeatureUpdated = new(
        Values.FeatureUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FlagUpdated = new(
        Values.FlagUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FlagRulesUpdated = new(
        Values.FlagRulesUpdated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyCreated = new(
        Values.CompanyCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem UserCreated = new(
        Values.UserCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem PlanCreated = new(
        Values.PlanCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem PlanEntitlementCreated = new(
        Values.PlanEntitlementCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyOverrideCreated = new(
        Values.CompanyOverrideCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FeatureCreated = new(
        Values.FeatureCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FlagCreated = new(
        Values.FlagCreated
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyDeleted = new(
        Values.CompanyDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem UserDeleted = new(
        Values.UserDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem PlanDeleted = new(
        Values.PlanDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem PlanEntitlementDeleted = new(
        Values.PlanEntitlementDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyOverrideDeleted = new(
        Values.CompanyOverrideDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FeatureDeleted = new(
        Values.FeatureDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem FlagDeleted = new(
        Values.FlagDeleted
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem TestSend = new(Values.TestSend);

    public static readonly UpdateWebhookRequestBodyRequestTypesItem SubscriptionTrialEnded = new(
        Values.SubscriptionTrialEnded
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem EntitlementLimitWarning = new(
        Values.EntitlementLimitWarning
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem EntitlementLimitReached = new(
        Values.EntitlementLimitReached
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem EntitlementSoftLimitWarning =
        new(Values.EntitlementSoftLimitWarning);

    public static readonly UpdateWebhookRequestBodyRequestTypesItem EntitlementSoftLimitReached =
        new(Values.EntitlementSoftLimitReached);

    public static readonly UpdateWebhookRequestBodyRequestTypesItem EntitlementTierLimitWarning =
        new(Values.EntitlementTierLimitWarning);

    public static readonly UpdateWebhookRequestBodyRequestTypesItem EntitlementTierLimitReached =
        new(Values.EntitlementTierLimitReached);

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CreditLimitWarning = new(
        Values.CreditLimitWarning
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CreditLimitReached = new(
        Values.CreditLimitReached
    );

    public static readonly UpdateWebhookRequestBodyRequestTypesItem CompanyPlanChange = new(
        Values.CompanyPlanChange
    );

    public UpdateWebhookRequestBodyRequestTypesItem(string value)
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
    public static UpdateWebhookRequestBodyRequestTypesItem FromCustom(string value)
    {
        return new UpdateWebhookRequestBodyRequestTypesItem(value);
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

    public static bool operator ==(
        UpdateWebhookRequestBodyRequestTypesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateWebhookRequestBodyRequestTypesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateWebhookRequestBodyRequestTypesItem value) =>
        value.Value;

    public static explicit operator UpdateWebhookRequestBodyRequestTypesItem(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CompanyUpdated = "company.updated";

        public const string UserUpdated = "user.updated";

        public const string PlanUpdated = "plan.updated";

        public const string PlanEntitlementUpdated = "plan.entitlement.updated";

        public const string CompanyOverrideUpdated = "company.override.updated";

        public const string FeatureUpdated = "feature.updated";

        public const string FlagUpdated = "flag.updated";

        public const string FlagRulesUpdated = "flag_rules.updated";

        public const string CompanyCreated = "company.created";

        public const string UserCreated = "user.created";

        public const string PlanCreated = "plan.created";

        public const string PlanEntitlementCreated = "plan.entitlement.created";

        public const string CompanyOverrideCreated = "company.override.created";

        public const string FeatureCreated = "feature.created";

        public const string FlagCreated = "flag.created";

        public const string CompanyDeleted = "company.deleted";

        public const string UserDeleted = "user.deleted";

        public const string PlanDeleted = "plan.deleted";

        public const string PlanEntitlementDeleted = "plan.entitlement.deleted";

        public const string CompanyOverrideDeleted = "company.override.deleted";

        public const string FeatureDeleted = "feature.deleted";

        public const string FlagDeleted = "flag.deleted";

        public const string TestSend = "test.send";

        public const string SubscriptionTrialEnded = "subscription.trial.ended";

        public const string EntitlementLimitWarning = "entitlement.limit.warning";

        public const string EntitlementLimitReached = "entitlement.limit.reached";

        public const string EntitlementSoftLimitWarning = "entitlement.soft_limit.warning";

        public const string EntitlementSoftLimitReached = "entitlement.soft_limit.reached";

        public const string EntitlementTierLimitWarning = "entitlement.tier_limit.warning";

        public const string EntitlementTierLimitReached = "entitlement.tier_limit.reached";

        public const string CreditLimitWarning = "credit.limit.warning";

        public const string CreditLimitReached = "credit.limit.reached";

        public const string CompanyPlanChange = "company.plan_change";
    }
}
