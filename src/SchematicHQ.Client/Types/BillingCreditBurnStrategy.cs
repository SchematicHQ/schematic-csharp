using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingCreditBurnStrategy>))]
[Serializable]
public readonly record struct BillingCreditBurnStrategy : IStringEnum
{
    public static readonly BillingCreditBurnStrategy ExpirationPriority = new(
        Values.ExpirationPriority
    );

    public static readonly BillingCreditBurnStrategy FirstInFirstOut = new(Values.FirstInFirstOut);

    public static readonly BillingCreditBurnStrategy LastInFirstOut = new(Values.LastInFirstOut);

    public static readonly BillingCreditBurnStrategy PlanFirstThenCreditBundlesFirstInFirstOut =
        new(Values.PlanFirstThenCreditBundlesFirstInFirstOut);

    public BillingCreditBurnStrategy(string value)
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
    public static BillingCreditBurnStrategy FromCustom(string value)
    {
        return new BillingCreditBurnStrategy(value);
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

    public static bool operator ==(BillingCreditBurnStrategy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditBurnStrategy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditBurnStrategy value) => value.Value;

    public static explicit operator BillingCreditBurnStrategy(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string ExpirationPriority = "expiration_priority";

        public const string FirstInFirstOut = "first_in_first_out";

        public const string LastInFirstOut = "last_in_first_out";

        public const string PlanFirstThenCreditBundlesFirstInFirstOut =
            "plan_first_then_credit_bundles_first_in_first_out";
    }
}
