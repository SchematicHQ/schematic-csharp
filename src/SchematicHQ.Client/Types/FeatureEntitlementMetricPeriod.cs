using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureEntitlementMetricPeriod>))]
[Serializable]
public readonly record struct FeatureEntitlementMetricPeriod : IStringEnum
{
    public static readonly FeatureEntitlementMetricPeriod AllTime = new(Values.AllTime);

    public static readonly FeatureEntitlementMetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly FeatureEntitlementMetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly FeatureEntitlementMetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public FeatureEntitlementMetricPeriod(string value)
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
    public static FeatureEntitlementMetricPeriod FromCustom(string value)
    {
        return new FeatureEntitlementMetricPeriod(value);
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

    public static bool operator ==(FeatureEntitlementMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FeatureEntitlementMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FeatureEntitlementMetricPeriod value) => value.Value;

    public static explicit operator FeatureEntitlementMetricPeriod(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AllTime = "all_time";

        public const string CurrentDay = "current_day";

        public const string CurrentMonth = "current_month";

        public const string CurrentWeek = "current_week";
    }
}
