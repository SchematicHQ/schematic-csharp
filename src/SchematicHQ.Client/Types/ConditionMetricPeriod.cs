using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ConditionMetricPeriod>))]
[Serializable]
public readonly record struct ConditionMetricPeriod : IStringEnum
{
    public static readonly ConditionMetricPeriod AllTime = new(Values.AllTime);

    public static readonly ConditionMetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly ConditionMetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly ConditionMetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public ConditionMetricPeriod(string value)
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
    public static ConditionMetricPeriod FromCustom(string value)
    {
        return new ConditionMetricPeriod(value);
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

    public static bool operator ==(ConditionMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ConditionMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ConditionMetricPeriod value) => value.Value;

    public static explicit operator ConditionMetricPeriod(string value) => new(value);

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
