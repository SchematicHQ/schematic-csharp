using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateEntitlementReqCommonMetricPeriodMonthReset>))]
[Serializable]
public readonly record struct CreateEntitlementReqCommonMetricPeriodMonthReset : IStringEnum
{
    public static readonly CreateEntitlementReqCommonMetricPeriodMonthReset FirstOfMonth = new(
        Values.FirstOfMonth
    );

    public static readonly CreateEntitlementReqCommonMetricPeriodMonthReset BillingCycle = new(
        Values.BillingCycle
    );

    public CreateEntitlementReqCommonMetricPeriodMonthReset(string value)
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
    public static CreateEntitlementReqCommonMetricPeriodMonthReset FromCustom(string value)
    {
        return new CreateEntitlementReqCommonMetricPeriodMonthReset(value);
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
        CreateEntitlementReqCommonMetricPeriodMonthReset value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEntitlementReqCommonMetricPeriodMonthReset value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateEntitlementReqCommonMetricPeriodMonthReset value
    ) => value.Value;

    public static explicit operator CreateEntitlementReqCommonMetricPeriodMonthReset(
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
