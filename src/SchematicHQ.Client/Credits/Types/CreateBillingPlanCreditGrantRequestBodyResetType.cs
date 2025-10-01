using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateBillingPlanCreditGrantRequestBodyResetType>))]
[Serializable]
public readonly record struct CreateBillingPlanCreditGrantRequestBodyResetType : IStringEnum
{
    public static readonly CreateBillingPlanCreditGrantRequestBodyResetType PlanPeriod = new(
        Values.PlanPeriod
    );

    public static readonly CreateBillingPlanCreditGrantRequestBodyResetType NoReset = new(
        Values.NoReset
    );

    public CreateBillingPlanCreditGrantRequestBodyResetType(string value)
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
    public static CreateBillingPlanCreditGrantRequestBodyResetType FromCustom(string value)
    {
        return new CreateBillingPlanCreditGrantRequestBodyResetType(value);
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
        CreateBillingPlanCreditGrantRequestBodyResetType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingPlanCreditGrantRequestBodyResetType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingPlanCreditGrantRequestBodyResetType value
    ) => value.Value;

    public static explicit operator CreateBillingPlanCreditGrantRequestBodyResetType(
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
