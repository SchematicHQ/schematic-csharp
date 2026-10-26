using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CheckoutProblemCode.CheckoutProblemCodeSerializer))]
[Serializable]
public readonly record struct CheckoutProblemCode : IStringEnum
{
    public static readonly CheckoutProblemCode AddOnConflictsWithPlan = new(
        Values.AddOnConflictsWithPlan
    );

    public static readonly CheckoutProblemCode AddOnIntervalMismatch = new(
        Values.AddOnIntervalMismatch
    );

    public static readonly CheckoutProblemCode AddOnRequiresBilledPlan = new(
        Values.AddOnRequiresBilledPlan
    );

    public static readonly CheckoutProblemCode AddOnUnavailable = new(Values.AddOnUnavailable);

    public static readonly CheckoutProblemCode BillingCycleAnchorConflictsWithTrial = new(
        Values.BillingCycleAnchorConflictsWithTrial
    );

    public static readonly CheckoutProblemCode BillingCycleAnchorMustBeFuture = new(
        Values.BillingCycleAnchorMustBeFuture
    );

    public static readonly CheckoutProblemCode BillingCycleAnchorNeedsNewSubscription = new(
        Values.BillingCycleAnchorNeedsNewSubscription
    );

    public static readonly CheckoutProblemCode BillingCycleAnchorWithoutPlan = new(
        Values.BillingCycleAnchorWithoutPlan
    );

    public static readonly CheckoutProblemCode BillingStartDateMustBePast = new(
        Values.BillingStartDateMustBePast
    );

    public static readonly CheckoutProblemCode BillingStartDateNeedsNewSubscription = new(
        Values.BillingStartDateNeedsNewSubscription
    );

    public static readonly CheckoutProblemCode BillingStartDateNeedsRenewalDate = new(
        Values.BillingStartDateNeedsRenewalDate
    );

    public static readonly CheckoutProblemCode BillingStartDateNotBeforeRenewalDate = new(
        Values.BillingStartDateNotBeforeRenewalDate
    );

    public static readonly CheckoutProblemCode BillingStartDateTooFarInPast = new(
        Values.BillingStartDateTooFarInPast
    );

    public static readonly CheckoutProblemCode CheckoutSuperseded = new(Values.CheckoutSuperseded);

    public static readonly CheckoutProblemCode CreditBundleIncompatible = new(
        Values.CreditBundleIncompatible
    );

    public static readonly CheckoutProblemCode CreditBundleUnavailable = new(
        Values.CreditBundleUnavailable
    );

    public static readonly CheckoutProblemCode CreditBundlesActivateOnPayment = new(
        Values.CreditBundlesActivateOnPayment
    );

    public static readonly CheckoutProblemCode CurrencyMismatch = new(Values.CurrencyMismatch);

    public static readonly CheckoutProblemCode CustomFieldRequired = new(
        Values.CustomFieldRequired
    );

    public static readonly CheckoutProblemCode DiscountIneligible = new(Values.DiscountIneligible);

    public static readonly CheckoutProblemCode DowngradeNotPermitted = new(
        Values.DowngradeNotPermitted
    );

    public static readonly CheckoutProblemCode EndDateMustBeFuture = new(
        Values.EndDateMustBeFuture
    );

    public static readonly CheckoutProblemCode EndDateNeedsNewSubscription = new(
        Values.EndDateNeedsNewSubscription
    );

    public static readonly CheckoutProblemCode EndDateNotAfterRenewalDate = new(
        Values.EndDateNotAfterRenewalDate
    );

    public static readonly CheckoutProblemCode FreeFirstPeriodActivatesOnPayment = new(
        Values.FreeFirstPeriodActivatesOnPayment
    );

    public static readonly CheckoutProblemCode InvoiceEmailRequired = new(
        Values.InvoiceEmailRequired
    );

    public static readonly CheckoutProblemCode InvoicedSubscriptionConflictsWithTrial = new(
        Values.InvoicedSubscriptionConflictsWithTrial
    );

    public static readonly CheckoutProblemCode InvoicedSubscriptionNeedsNewSubscription = new(
        Values.InvoicedSubscriptionNeedsNewSubscription
    );

    public static readonly CheckoutProblemCode InvoicedSubscriptionRequiresPlan = new(
        Values.InvoicedSubscriptionRequiresPlan
    );

    public static readonly CheckoutProblemCode OptInRequired = new(Values.OptInRequired);

    public static readonly CheckoutProblemCode PayInAdvancePriceInvalid = new(
        Values.PayInAdvancePriceInvalid
    );

    public static readonly CheckoutProblemCode PayInAdvanceRequired = new(
        Values.PayInAdvanceRequired
    );

    public static readonly CheckoutProblemCode PaymentMethodRequired = new(
        Values.PaymentMethodRequired
    );

    public static readonly CheckoutProblemCode PlanNotTrialable = new(Values.PlanNotTrialable);

    public static readonly CheckoutProblemCode PlanUnavailable = new(Values.PlanUnavailable);

    public static readonly CheckoutProblemCode PriceUnavailable = new(Values.PriceUnavailable);

    public static readonly CheckoutProblemCode ProrateFirstPeriodWithoutAnchor = new(
        Values.ProrateFirstPeriodWithoutAnchor
    );

    public static readonly CheckoutProblemCode ProviderRejected = new(Values.ProviderRejected);

    public static readonly CheckoutProblemCode ScheduledChangeBlocksPurchase = new(
        Values.ScheduledChangeBlocksPurchase
    );

    public static readonly CheckoutProblemCode ScheduledChangePending = new(
        Values.ScheduledChangePending
    );

    public static readonly CheckoutProblemCode SelectionInvalid = new(Values.SelectionInvalid);

    public static readonly CheckoutProblemCode SubscriptionCurrencyChange = new(
        Values.SubscriptionCurrencyChange
    );

    public static readonly CheckoutProblemCode TrialEndMustBeFuture = new(
        Values.TrialEndMustBeFuture
    );

    public static readonly CheckoutProblemCode TrialEndTooFar = new(Values.TrialEndTooFar);

    public static readonly CheckoutProblemCode TrialExcludesAddOns = new(
        Values.TrialExcludesAddOns
    );

    public static readonly CheckoutProblemCode TrialExcludesUsageBased = new(
        Values.TrialExcludesUsageBased
    );

    public static readonly CheckoutProblemCode UsagePriceMissingForInterval = new(
        Values.UsagePriceMissingForInterval
    );

    public static readonly CheckoutProblemCode UsageOverLimit = new(Values.UsageOverLimit);

    public CheckoutProblemCode(string value)
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
    public static CheckoutProblemCode FromCustom(string value)
    {
        return new CheckoutProblemCode(value);
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

    public static bool operator ==(CheckoutProblemCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CheckoutProblemCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CheckoutProblemCode value) => value.Value;

    public static explicit operator CheckoutProblemCode(string value) => new(value);

    internal class CheckoutProblemCodeSerializer : JsonConverter<CheckoutProblemCode>
    {
        public override CheckoutProblemCode Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new CheckoutProblemCode(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CheckoutProblemCode value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CheckoutProblemCode ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new CheckoutProblemCode(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CheckoutProblemCode value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AddOnConflictsWithPlan = "add_on_conflicts_with_plan";

        public const string AddOnIntervalMismatch = "add_on_interval_mismatch";

        public const string AddOnRequiresBilledPlan = "add_on_requires_billed_plan";

        public const string AddOnUnavailable = "add_on_unavailable";

        public const string BillingCycleAnchorConflictsWithTrial =
            "billing_cycle_anchor_conflicts_with_trial";

        public const string BillingCycleAnchorMustBeFuture = "billing_cycle_anchor_must_be_future";

        public const string BillingCycleAnchorNeedsNewSubscription =
            "billing_cycle_anchor_needs_new_subscription";

        public const string BillingCycleAnchorWithoutPlan = "billing_cycle_anchor_without_plan";

        public const string BillingStartDateMustBePast = "billing_start_date_must_be_past";

        public const string BillingStartDateNeedsNewSubscription =
            "billing_start_date_needs_new_subscription";

        public const string BillingStartDateNeedsRenewalDate =
            "billing_start_date_needs_renewal_date";

        public const string BillingStartDateNotBeforeRenewalDate =
            "billing_start_date_not_before_renewal_date";

        public const string BillingStartDateTooFarInPast = "billing_start_date_too_far_in_past";

        public const string CheckoutSuperseded = "checkout_superseded";

        public const string CreditBundleIncompatible = "credit_bundle_incompatible";

        public const string CreditBundleUnavailable = "credit_bundle_unavailable";

        public const string CreditBundlesActivateOnPayment = "credit_bundles_activate_on_payment";

        public const string CurrencyMismatch = "currency_mismatch";

        public const string CustomFieldRequired = "custom_field_required";

        public const string DiscountIneligible = "discount_ineligible";

        public const string DowngradeNotPermitted = "downgrade_not_permitted";

        public const string EndDateMustBeFuture = "end_date_must_be_future";

        public const string EndDateNeedsNewSubscription = "end_date_needs_new_subscription";

        public const string EndDateNotAfterRenewalDate = "end_date_not_after_renewal_date";

        public const string FreeFirstPeriodActivatesOnPayment =
            "free_first_period_activates_on_payment";

        public const string InvoiceEmailRequired = "invoice_email_required";

        public const string InvoicedSubscriptionConflictsWithTrial =
            "invoiced_subscription_conflicts_with_trial";

        public const string InvoicedSubscriptionNeedsNewSubscription =
            "invoiced_subscription_needs_new_subscription";

        public const string InvoicedSubscriptionRequiresPlan =
            "invoiced_subscription_requires_plan";

        public const string OptInRequired = "opt_in_required";

        public const string PayInAdvancePriceInvalid = "pay_in_advance_price_invalid";

        public const string PayInAdvanceRequired = "pay_in_advance_required";

        public const string PaymentMethodRequired = "payment_method_required";

        public const string PlanNotTrialable = "plan_not_trialable";

        public const string PlanUnavailable = "plan_unavailable";

        public const string PriceUnavailable = "price_unavailable";

        public const string ProrateFirstPeriodWithoutAnchor = "prorate_first_period_without_anchor";

        public const string ProviderRejected = "provider_rejected";

        public const string ScheduledChangeBlocksPurchase = "scheduled_change_blocks_purchase";

        public const string ScheduledChangePending = "scheduled_change_pending";

        public const string SelectionInvalid = "selection_invalid";

        public const string SubscriptionCurrencyChange = "subscription_currency_change";

        public const string TrialEndMustBeFuture = "trial_end_must_be_future";

        public const string TrialEndTooFar = "trial_end_too_far";

        public const string TrialExcludesAddOns = "trial_excludes_add_ons";

        public const string TrialExcludesUsageBased = "trial_excludes_usage_based";

        public const string UsagePriceMissingForInterval = "usage_price_missing_for_interval";

        public const string UsageOverLimit = "usage_over_limit";
    }
}
