using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset>)
)]
[Serializable]
public readonly record struct CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset
    : IStringEnum
{
    public static readonly CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset FirstOfMonth =
        new(Values.FirstOfMonth);

    public static readonly CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset BillingCycle =
        new(Values.BillingCycle);

    public CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset(string value)
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
    public static CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset FromCustom(
        string value
    )
    {
        return new CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset(value);
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
        CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset value
    ) => value.Value;

    public static explicit operator CreateEntitlementInBundleRequestBodyMetricPeriodMonthReset(
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
