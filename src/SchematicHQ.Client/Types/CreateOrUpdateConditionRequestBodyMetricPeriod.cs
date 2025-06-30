using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateOrUpdateConditionRequestBodyMetricPeriod>))]
[Serializable]
public readonly record struct CreateOrUpdateConditionRequestBodyMetricPeriod : IStringEnum
{
    public static readonly CreateOrUpdateConditionRequestBodyMetricPeriod AllTime = new(
        Values.AllTime
    );

    public static readonly CreateOrUpdateConditionRequestBodyMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly CreateOrUpdateConditionRequestBodyMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public static readonly CreateOrUpdateConditionRequestBodyMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public CreateOrUpdateConditionRequestBodyMetricPeriod(string value)
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
    public static CreateOrUpdateConditionRequestBodyMetricPeriod FromCustom(string value)
    {
        return new CreateOrUpdateConditionRequestBodyMetricPeriod(value);
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
        CreateOrUpdateConditionRequestBodyMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateOrUpdateConditionRequestBodyMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateOrUpdateConditionRequestBodyMetricPeriod value) =>
        value.Value;

    public static explicit operator CreateOrUpdateConditionRequestBodyMetricPeriod(string value) =>
        new(value);

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
