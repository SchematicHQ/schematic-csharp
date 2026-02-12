using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ScheduledDowngradeConfigBehavior>))]
[Serializable]
public readonly record struct ScheduledDowngradeConfigBehavior : IStringEnum
{
    public static readonly ScheduledDowngradeConfigBehavior EndOfBillingPeriod = new(
        Values.EndOfBillingPeriod
    );

    public static readonly ScheduledDowngradeConfigBehavior None = new(Values.None);

    public ScheduledDowngradeConfigBehavior(string value)
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
    public static ScheduledDowngradeConfigBehavior FromCustom(string value)
    {
        return new ScheduledDowngradeConfigBehavior(value);
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

    public static bool operator ==(ScheduledDowngradeConfigBehavior value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ScheduledDowngradeConfigBehavior value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ScheduledDowngradeConfigBehavior value) => value.Value;

    public static explicit operator ScheduledDowngradeConfigBehavior(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string EndOfBillingPeriod = "end_of_billing_period";

        public const string None = "none";
    }
}
