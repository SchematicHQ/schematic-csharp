using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineCheckFlagResultFeatureUsagePeriod>))]
[Serializable]
public readonly record struct RulesengineCheckFlagResultFeatureUsagePeriod : IStringEnum
{
    public static readonly RulesengineCheckFlagResultFeatureUsagePeriod AllTime = new(
        Values.AllTime
    );

    public static readonly RulesengineCheckFlagResultFeatureUsagePeriod CurrentDay = new(
        Values.CurrentDay
    );

    public static readonly RulesengineCheckFlagResultFeatureUsagePeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly RulesengineCheckFlagResultFeatureUsagePeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public RulesengineCheckFlagResultFeatureUsagePeriod(string value)
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
    public static RulesengineCheckFlagResultFeatureUsagePeriod FromCustom(string value)
    {
        return new RulesengineCheckFlagResultFeatureUsagePeriod(value);
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
        RulesengineCheckFlagResultFeatureUsagePeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        RulesengineCheckFlagResultFeatureUsagePeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineCheckFlagResultFeatureUsagePeriod value) =>
        value.Value;

    public static explicit operator RulesengineCheckFlagResultFeatureUsagePeriod(string value) =>
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
