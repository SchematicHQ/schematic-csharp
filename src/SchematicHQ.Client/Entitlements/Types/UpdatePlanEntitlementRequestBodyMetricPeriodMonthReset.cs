using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset>)
)]
[Serializable]
public readonly record struct UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset : IStringEnum
{
    public static readonly UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset FirstOfMonth =
        new(Values.FirstOfMonth);

    public static readonly UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset BillingCycle =
        new(Values.BillingCycle);

    public UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset(string value)
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
    public static UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset FromCustom(string value)
    {
        return new UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset(value);
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
        UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset value
    ) => value.Value;

    public static explicit operator UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string FirstOfMonth = "first_of_month";

        public const string BillingCycle = "billing_cycle";
    }
}
