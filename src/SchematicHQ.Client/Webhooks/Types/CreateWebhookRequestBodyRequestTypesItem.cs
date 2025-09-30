using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateWebhookRequestBodyRequestTypesItem>))]
[Serializable]
public readonly record struct CreateWebhookRequestBodyRequestTypesItem : IStringEnum
{
    public static readonly CreateWebhookRequestBodyRequestTypesItem CompanyUpdated = new(
        Values.CompanyUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem UserUpdated = new(
        Values.UserUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem PlanUpdated = new(
        Values.PlanUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem PlanEntitlementUpdated = new(
        Values.PlanEntitlementUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem CompanyOverrideUpdated = new(
        Values.CompanyOverrideUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FeatureUpdated = new(
        Values.FeatureUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FlagUpdated = new(
        Values.FlagUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FlagRulesUpdated = new(
        Values.FlagRulesUpdated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem CompanyCreated = new(
        Values.CompanyCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem UserCreated = new(
        Values.UserCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem PlanCreated = new(
        Values.PlanCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem PlanEntitlementCreated = new(
        Values.PlanEntitlementCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem CompanyOverrideCreated = new(
        Values.CompanyOverrideCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FeatureCreated = new(
        Values.FeatureCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FlagCreated = new(
        Values.FlagCreated
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem CompanyDeleted = new(
        Values.CompanyDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem UserDeleted = new(
        Values.UserDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem PlanDeleted = new(
        Values.PlanDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem PlanEntitlementDeleted = new(
        Values.PlanEntitlementDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem CompanyOverrideDeleted = new(
        Values.CompanyOverrideDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FeatureDeleted = new(
        Values.FeatureDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem FlagDeleted = new(
        Values.FlagDeleted
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem TestSend = new(Values.TestSend);

    public static readonly CreateWebhookRequestBodyRequestTypesItem SubscriptionTrialEnded = new(
        Values.SubscriptionTrialEnded
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem EntitlementLimitWarning = new(
        Values.EntitlementLimitWarning
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem EntitlementLimitReached = new(
        Values.EntitlementLimitReached
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem EntitlementSoftLimitWarning =
        new(Values.EntitlementSoftLimitWarning);

    public static readonly CreateWebhookRequestBodyRequestTypesItem EntitlementSoftLimitReached =
        new(Values.EntitlementSoftLimitReached);

    public static readonly CreateWebhookRequestBodyRequestTypesItem CreditLimitWarning = new(
        Values.CreditLimitWarning
    );

    public static readonly CreateWebhookRequestBodyRequestTypesItem CreditLimitReached = new(
        Values.CreditLimitReached
    );

    public CreateWebhookRequestBodyRequestTypesItem(string value)
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
    public static CreateWebhookRequestBodyRequestTypesItem FromCustom(string value)
    {
        return new CreateWebhookRequestBodyRequestTypesItem(value);
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
        CreateWebhookRequestBodyRequestTypesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateWebhookRequestBodyRequestTypesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateWebhookRequestBodyRequestTypesItem value) =>
        value.Value;

    public static explicit operator CreateWebhookRequestBodyRequestTypesItem(string value) =>
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

        public const string CreditLimitWarning = "credit.limit.warning";

        public const string CreditLimitReached = "credit.limit.reached";
    }
}
