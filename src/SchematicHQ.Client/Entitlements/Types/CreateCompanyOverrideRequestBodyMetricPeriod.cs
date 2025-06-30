using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateCompanyOverrideRequestBodyMetricPeriod>))]
[Serializable]
public readonly record struct CreateCompanyOverrideRequestBodyMetricPeriod : IStringEnum
{
    public static readonly CreateCompanyOverrideRequestBodyMetricPeriod AllTime = new(
        Values.AllTime
    );

    public static readonly CreateCompanyOverrideRequestBodyMetricPeriod CurrentMonth = new(
        Values.CurrentMonth
    );

    public static readonly CreateCompanyOverrideRequestBodyMetricPeriod CurrentWeek = new(
        Values.CurrentWeek
    );

    public static readonly CreateCompanyOverrideRequestBodyMetricPeriod CurrentDay = new(
        Values.CurrentDay
    );

    public CreateCompanyOverrideRequestBodyMetricPeriod(string value)
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
    public static CreateCompanyOverrideRequestBodyMetricPeriod FromCustom(string value)
    {
        return new CreateCompanyOverrideRequestBodyMetricPeriod(value);
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
        CreateCompanyOverrideRequestBodyMetricPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateCompanyOverrideRequestBodyMetricPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateCompanyOverrideRequestBodyMetricPeriod value) =>
        value.Value;

    public static explicit operator CreateCompanyOverrideRequestBodyMetricPeriod(string value) =>
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
