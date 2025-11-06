using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateBillingPlanCreditGrantRequestBodyResetType>))]
[Serializable]
public readonly record struct UpdateBillingPlanCreditGrantRequestBodyResetType : IStringEnum
{
    public static readonly UpdateBillingPlanCreditGrantRequestBodyResetType PlanPeriod = new(
        Values.PlanPeriod
    );

    public static readonly UpdateBillingPlanCreditGrantRequestBodyResetType NoReset = new(
        Values.NoReset
    );

    public UpdateBillingPlanCreditGrantRequestBodyResetType(string value)
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
    public static UpdateBillingPlanCreditGrantRequestBodyResetType FromCustom(string value)
    {
        return new UpdateBillingPlanCreditGrantRequestBodyResetType(value);
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
        UpdateBillingPlanCreditGrantRequestBodyResetType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateBillingPlanCreditGrantRequestBodyResetType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        UpdateBillingPlanCreditGrantRequestBodyResetType value
    ) => value.Value;

    public static explicit operator UpdateBillingPlanCreditGrantRequestBodyResetType(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PlanPeriod = "plan_period";

        public const string NoReset = "no_reset";
    }
}
