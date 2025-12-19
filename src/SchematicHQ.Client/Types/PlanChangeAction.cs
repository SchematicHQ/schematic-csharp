using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeAction>))]
[Serializable]
public readonly record struct PlanChangeAction : IStringEnum
{
    public static readonly PlanChangeAction Checkout = new(Values.Checkout);

    public static readonly PlanChangeAction CompanyUpsert = new(Values.CompanyUpsert);

    public static readonly PlanChangeAction FallbackPlan = new(Values.FallbackPlan);

    public static readonly PlanChangeAction ManagePlan = new(Values.ManagePlan);

    public static readonly PlanChangeAction Migration = new(Values.Migration);

    public static readonly PlanChangeAction PlanBillingProductChanged = new(
        Values.PlanBillingProductChanged
    );

    public static readonly PlanChangeAction PlanDeleted = new(Values.PlanDeleted);

    public static readonly PlanChangeAction PlanTraitChange = new(Values.PlanTraitChange);

    public static readonly PlanChangeAction Quickstart = new(Values.Quickstart);

    public static readonly PlanChangeAction SubscriptionChange = new(Values.SubscriptionChange);

    public PlanChangeAction(string value)
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
    public static PlanChangeAction FromCustom(string value)
    {
        return new PlanChangeAction(value);
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

    public static bool operator ==(PlanChangeAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeAction value) => value.Value;

    public static explicit operator PlanChangeAction(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Checkout = "checkout";

        public const string CompanyUpsert = "company_upsert";

        public const string FallbackPlan = "fallback_plan";

        public const string ManagePlan = "manage_plan";

        public const string Migration = "migration";

        public const string PlanBillingProductChanged = "plan_billing_product_changed";

        public const string PlanDeleted = "plan_deleted";

        public const string PlanTraitChange = "plan_trait_change";

        public const string Quickstart = "quickstart";

        public const string SubscriptionChange = "subscription_change";
    }
}
