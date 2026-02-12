using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineFeatureEntitlementMetricPeriod>))]
[Serializable]
public readonly record struct RulesengineFeatureEntitlementMetricPeriod : IStringEnum
{
    public static readonly RulesengineFeatureEntitlementMetricPeriod AllTime = new(Values.AllTime);

    public static readonly RulesengineFeatureEntitlementMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public static readonly RulesengineFeatureEntitlementMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly RulesengineFeatureEntitlementMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public RulesengineFeatureEntitlementMetricPeriod(string value)
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
    public static RulesengineFeatureEntitlementMetricPeriod FromCustom(string value)
    {
        return new RulesengineFeatureEntitlementMetricPeriod(value);
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
        RulesengineFeatureEntitlementMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        RulesengineFeatureEntitlementMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineFeatureEntitlementMetricPeriod value) =>
        value.Value;

    public static explicit operator RulesengineFeatureEntitlementMetricPeriod(string value) =>
        new(value);

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
