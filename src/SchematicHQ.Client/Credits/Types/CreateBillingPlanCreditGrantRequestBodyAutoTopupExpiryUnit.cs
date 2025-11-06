using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit>)
)]
[Serializable]
public readonly record struct CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit
    : IStringEnum
{
    public static readonly CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit Days = new(
        Values.Days
    );

    public static readonly CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit BillingPeriods =
        new(Values.BillingPeriods);

    public CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit(string value)
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
    public static CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit FromCustom(
        string value
    )
    {
        return new CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit(value);
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
        CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit value
    ) => value.Value;

    public static explicit operator CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryUnit(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Days = "days";

        public const string BillingPeriods = "billing_periods";
    }
}
