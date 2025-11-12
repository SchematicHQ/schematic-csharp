using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CompanyMetricPeriod>))]
[Serializable]
public readonly record struct CompanyMetricPeriod : IStringEnum
{
    public static readonly CompanyMetricPeriod AllTime = new(Values.AllTime);

    public static readonly CompanyMetricPeriod CurrentDay = new(Values.CurrentDay);

    public static readonly CompanyMetricPeriod CurrentMonth = new(Values.CurrentMonth);

    public static readonly CompanyMetricPeriod CurrentWeek = new(Values.CurrentWeek);

    public CompanyMetricPeriod(string value)
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
    public static CompanyMetricPeriod FromCustom(string value)
    {
        return new CompanyMetricPeriod(value);
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

    public static bool operator ==(CompanyMetricPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CompanyMetricPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CompanyMetricPeriod value) => value.Value;

    public static explicit operator CompanyMetricPeriod(string value) => new(value);

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
