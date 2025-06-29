using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateWebhookRequestBodyRequestTypesItem>))]
public enum UpdateWebhookRequestBodyRequestTypesItem
{
    [EnumMember(Value = "company.updated")]
    CompanyUpdated,

    [EnumMember(Value = "user.updated")]
    UserUpdated,

    [EnumMember(Value = "plan.updated")]
    PlanUpdated,

    [EnumMember(Value = "plan.entitlement.updated")]
    PlanEntitlementUpdated,

    [EnumMember(Value = "company.override.updated")]
    CompanyOverrideUpdated,

    [EnumMember(Value = "feature.updated")]
    FeatureUpdated,

    [EnumMember(Value = "flag.updated")]
    FlagUpdated,

    [EnumMember(Value = "flag_rules.updated")]
    FlagRulesUpdated,

    [EnumMember(Value = "company.created")]
    CompanyCreated,

    [EnumMember(Value = "user.created")]
    UserCreated,

    [EnumMember(Value = "plan.created")]
    PlanCreated,

    [EnumMember(Value = "plan.entitlement.created")]
    PlanEntitlementCreated,

    [EnumMember(Value = "company.override.created")]
    CompanyOverrideCreated,

    [EnumMember(Value = "feature.created")]
    FeatureCreated,

    [EnumMember(Value = "flag.created")]
    FlagCreated,

    [EnumMember(Value = "company.deleted")]
    CompanyDeleted,

    [EnumMember(Value = "user.deleted")]
    UserDeleted,

    [EnumMember(Value = "plan.deleted")]
    PlanDeleted,

    [EnumMember(Value = "plan.entitlement.deleted")]
    PlanEntitlementDeleted,

    [EnumMember(Value = "company.override.deleted")]
    CompanyOverrideDeleted,

    [EnumMember(Value = "feature.deleted")]
    FeatureDeleted,

    [EnumMember(Value = "flag.deleted")]
    FlagDeleted,

    [EnumMember(Value = "test.send")]
    TestSend,

    [EnumMember(Value = "subscription.trial.ended")]
    SubscriptionTrialEnded,

    [EnumMember(Value = "entitlement.limit.warning")]
    EntitlementLimitWarning,

    [EnumMember(Value = "entitlement.limit.reached")]
    EntitlementLimitReached,

    [EnumMember(Value = "entitlement.soft_limit.warning")]
    EntitlementSoftLimitWarning,

    [EnumMember(Value = "entitlement.soft_limit.reached")]
    EntitlementSoftLimitReached,
}
