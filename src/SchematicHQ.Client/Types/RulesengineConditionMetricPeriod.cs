using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineConditionMetricPeriod>))]
[Serializable]
public readonly record struct RulesengineConditionMetricPeriod : IStringEnum
{
    public static readonly RulesengineConditionMetricPeriod AllTime = new(Values.AllTime);

    public static readonly RulesengineConditionMetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly RulesengineConditionMetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly RulesengineConditionMetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public RulesengineConditionMetricPeriod(string value)
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
    public static RulesengineConditionMetricPeriod FromCustom(string value)
    {
        return new RulesengineConditionMetricPeriod(value);
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

    public static bool operator ==(RulesengineConditionMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineConditionMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineConditionMetricPeriod value) => value.Value;

    public static explicit operator RulesengineConditionMetricPeriod(string value) => new(value);

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
