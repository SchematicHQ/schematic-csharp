using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateBillingPlanCreditGrantRequestBodyExpiryUnit>))]
[Serializable]
public readonly record struct CreateBillingPlanCreditGrantRequestBodyExpiryUnit : IStringEnum
{
    public static readonly CreateBillingPlanCreditGrantRequestBodyExpiryUnit Days = new(
        Values.Days
    );

    public static readonly CreateBillingPlanCreditGrantRequestBodyExpiryUnit BillingPeriods = new(
        Values.BillingPeriods
    );

    public CreateBillingPlanCreditGrantRequestBodyExpiryUnit(string value)
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
    public static CreateBillingPlanCreditGrantRequestBodyExpiryUnit FromCustom(string value)
    {
        return new CreateBillingPlanCreditGrantRequestBodyExpiryUnit(value);
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
        CreateBillingPlanCreditGrantRequestBodyExpiryUnit value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingPlanCreditGrantRequestBodyExpiryUnit value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingPlanCreditGrantRequestBodyExpiryUnit value
    ) => value.Value;

    public static explicit operator CreateBillingPlanCreditGrantRequestBodyExpiryUnit(
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
