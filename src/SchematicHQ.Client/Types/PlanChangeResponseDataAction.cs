using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeResponseDataAction>))]
[Serializable]
public readonly record struct PlanChangeResponseDataAction : IStringEnum
{
    public static readonly PlanChangeResponseDataAction Checkout = new(Values.Checkout);

    public static readonly PlanChangeResponseDataAction CompanyUpsert = new(Values.CompanyUpsert);

    public static readonly PlanChangeResponseDataAction FallbackPlan = new(Values.FallbackPlan);

    public static readonly PlanChangeResponseDataAction ManagePlan = new(Values.ManagePlan);

    public static readonly PlanChangeResponseDataAction Migration = new(Values.Migration);

    public static readonly PlanChangeResponseDataAction PlanBillingProductChanged = new(
        Values.PlanBillingProductChanged
    );

    public static readonly PlanChangeResponseDataAction PlanDeleted = new(Values.PlanDeleted);

    public static readonly PlanChangeResponseDataAction Quickstart = new(Values.Quickstart);

    public static readonly PlanChangeResponseDataAction SubscriptionChange = new(
        Values.SubscriptionChange
    );

    public PlanChangeResponseDataAction(string value)
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
    public static PlanChangeResponseDataAction FromCustom(string value)
    {
        return new PlanChangeResponseDataAction(value);
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

    public static bool operator ==(PlanChangeResponseDataAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeResponseDataAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeResponseDataAction value) => value.Value;

    public static explicit operator PlanChangeResponseDataAction(string value) => new(value);

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

        public const string Quickstart = "quickstart";

        public const string SubscriptionChange = "subscription_change";
    }
}
