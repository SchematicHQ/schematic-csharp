using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateEntitlementInBundleRequestBodyMetricPeriod>))]
[Serializable]
public readonly record struct CreateEntitlementInBundleRequestBodyMetricPeriod : IStringEnum
{
    public static readonly CreateEntitlementInBundleRequestBodyMetricPeriod AllTime = new(
        Values.AllTime
    );

    public static readonly CreateEntitlementInBundleRequestBodyMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly CreateEntitlementInBundleRequestBodyMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public static readonly CreateEntitlementInBundleRequestBodyMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public CreateEntitlementInBundleRequestBodyMetricPeriod(string value)
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
    public static CreateEntitlementInBundleRequestBodyMetricPeriod FromCustom(string value)
    {
        return new CreateEntitlementInBundleRequestBodyMetricPeriod(value);
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
        CreateEntitlementInBundleRequestBodyMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEntitlementInBundleRequestBodyMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateEntitlementInBundleRequestBodyMetricPeriod value
    ) => value.Value;

    public static explicit operator CreateEntitlementInBundleRequestBodyMetricPeriod(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AllTime = "all_time";

        public const string CurrentMonth = "current_month";

        public const string CurrentWeek = "current_week";

        public const string CurrentDay = "current_day";
    }
}
