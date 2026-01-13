using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingCreditGrantZeroedOutReason>))]
[Serializable]
public readonly record struct BillingCreditGrantZeroedOutReason : IStringEnum
{
    public static readonly BillingCreditGrantZeroedOutReason Expired = new(Values.Expired);

    public static readonly BillingCreditGrantZeroedOutReason Manual = new(Values.Manual);

    public static readonly BillingCreditGrantZeroedOutReason PlanChange = new(Values.PlanChange);

    public static readonly BillingCreditGrantZeroedOutReason PlanPeriodReset = new(
        Values.PlanPeriodReset
    );

    public BillingCreditGrantZeroedOutReason(string value)
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
    public static BillingCreditGrantZeroedOutReason FromCustom(string value)
    {
        return new BillingCreditGrantZeroedOutReason(value);
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

    public static bool operator ==(BillingCreditGrantZeroedOutReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditGrantZeroedOutReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditGrantZeroedOutReason value) => value.Value;

    public static explicit operator BillingCreditGrantZeroedOutReason(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Expired = "expired";

        public const string Manual = "manual";

        public const string PlanChange = "plan_change";

        public const string PlanPeriodReset = "plan_period_reset";
    }
}
