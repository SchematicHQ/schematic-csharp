using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateBillingPlanCreditGrantRequestBodyResetStart>))]
[Serializable]
public readonly record struct UpdateBillingPlanCreditGrantRequestBodyResetStart : IStringEnum
{
    public static readonly UpdateBillingPlanCreditGrantRequestBodyResetStart BillingPeriod = new(
        Values.BillingPeriod
    );

    public static readonly UpdateBillingPlanCreditGrantRequestBodyResetStart FirstOfMonth = new(
        Values.FirstOfMonth
    );

    public UpdateBillingPlanCreditGrantRequestBodyResetStart(string value)
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
    public static UpdateBillingPlanCreditGrantRequestBodyResetStart FromCustom(string value)
    {
        return new UpdateBillingPlanCreditGrantRequestBodyResetStart(value);
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
        UpdateBillingPlanCreditGrantRequestBodyResetStart value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateBillingPlanCreditGrantRequestBodyResetStart value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        UpdateBillingPlanCreditGrantRequestBodyResetStart value
    ) => value.Value;

    public static explicit operator UpdateBillingPlanCreditGrantRequestBodyResetStart(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string BillingPeriod = "billing_period";

        public const string FirstOfMonth = "first_of_month";
    }
}
