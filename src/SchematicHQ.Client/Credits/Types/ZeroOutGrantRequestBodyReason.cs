using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ZeroOutGrantRequestBodyReason>))]
[Serializable]
public readonly record struct ZeroOutGrantRequestBodyReason : IStringEnum
{
    public static readonly ZeroOutGrantRequestBodyReason PlanChange = new(Values.PlanChange);

    public static readonly ZeroOutGrantRequestBodyReason Manual = new(Values.Manual);

    public static readonly ZeroOutGrantRequestBodyReason PlanPeriodReset = new(
        Values.PlanPeriodReset
    );

    public static readonly ZeroOutGrantRequestBodyReason Expired = new(Values.Expired);

    public ZeroOutGrantRequestBodyReason(string value)
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
    public static ZeroOutGrantRequestBodyReason FromCustom(string value)
    {
        return new ZeroOutGrantRequestBodyReason(value);
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

    public static bool operator ==(ZeroOutGrantRequestBodyReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ZeroOutGrantRequestBodyReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ZeroOutGrantRequestBodyReason value) => value.Value;

    public static explicit operator ZeroOutGrantRequestBodyReason(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PlanChange = "plan_change";

        public const string Manual = "manual";

        public const string PlanPeriodReset = "plan_period_reset";

        public const string Expired = "expired";
    }
}
