using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ScheduledCheckoutStatus>))]
[Serializable]
public readonly record struct ScheduledCheckoutStatus : IStringEnum
{
    public static readonly ScheduledCheckoutStatus Cancelled = new(Values.Cancelled);

    public static readonly ScheduledCheckoutStatus Error = new(Values.Error);

    public static readonly ScheduledCheckoutStatus Executing = new(Values.Executing);

    public static readonly ScheduledCheckoutStatus Pending = new(Values.Pending);

    public static readonly ScheduledCheckoutStatus Success = new(Values.Success);

    public ScheduledCheckoutStatus(string value)
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
    public static ScheduledCheckoutStatus FromCustom(string value)
    {
        return new ScheduledCheckoutStatus(value);
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

    public static bool operator ==(ScheduledCheckoutStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ScheduledCheckoutStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ScheduledCheckoutStatus value) => value.Value;

    public static explicit operator ScheduledCheckoutStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Cancelled = "cancelled";

        public const string Error = "error";

        public const string Executing = "executing";

        public const string Pending = "pending";

        public const string Success = "success";
    }
}
