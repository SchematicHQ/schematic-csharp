using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingPlanCreditGrantResetType>))]
[Serializable]
public readonly record struct BillingPlanCreditGrantResetType : IStringEnum
{
    public static readonly BillingPlanCreditGrantResetType NoReset = new(Values.NoReset);

    public static readonly BillingPlanCreditGrantResetType PlanPeriod = new(Values.PlanPeriod);

    public BillingPlanCreditGrantResetType(string value)
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
    public static BillingPlanCreditGrantResetType FromCustom(string value)
    {
        return new BillingPlanCreditGrantResetType(value);
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

    public static bool operator ==(BillingPlanCreditGrantResetType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingPlanCreditGrantResetType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingPlanCreditGrantResetType value) => value.Value;

    public static explicit operator BillingPlanCreditGrantResetType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string NoReset = "no_reset";

        public const string PlanPeriod = "plan_period";
    }
}
