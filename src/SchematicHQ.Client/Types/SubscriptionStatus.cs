using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<SubscriptionStatus>))]
[Serializable]
public readonly record struct SubscriptionStatus : IStringEnum
{
    public static readonly SubscriptionStatus Active = new(Values.Active);

    public static readonly SubscriptionStatus Canceled = new(Values.Canceled);

    public static readonly SubscriptionStatus Expired = new(Values.Expired);

    public static readonly SubscriptionStatus Incomplete = new(Values.Incomplete);

    public static readonly SubscriptionStatus IncompleteExpired = new(Values.IncompleteExpired);

    public static readonly SubscriptionStatus PastDue = new(Values.PastDue);

    public static readonly SubscriptionStatus Paused = new(Values.Paused);

    public static readonly SubscriptionStatus Trialing = new(Values.Trialing);

    public static readonly SubscriptionStatus Unpaid = new(Values.Unpaid);

    public SubscriptionStatus(string value)
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
    public static SubscriptionStatus FromCustom(string value)
    {
        return new SubscriptionStatus(value);
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

    public static bool operator ==(SubscriptionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionStatus value) => value.Value;

    public static explicit operator SubscriptionStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Active = "active";

        public const string Canceled = "canceled";

        public const string Expired = "expired";

        public const string Incomplete = "incomplete";

        public const string IncompleteExpired = "incomplete_expired";

        public const string PastDue = "past_due";

        public const string Paused = "paused";

        public const string Trialing = "trialing";

        public const string Unpaid = "unpaid";
    }
}
