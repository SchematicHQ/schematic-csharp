using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset>)
)]
[Serializable]
public readonly record struct CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset : IStringEnum
{
    public static readonly CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset FirstOfMonth =
        new(Values.FirstOfMonth);

    public static readonly CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset BillingCycle =
        new(Values.BillingCycle);

    public CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset(string value)
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
    public static CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset FromCustom(string value)
    {
        return new CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset(value);
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
        CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset value
    ) => value.Value;

    public static explicit operator CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset(
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
