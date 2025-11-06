using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeResponseDataSubscriptionChangeAction>))]
[Serializable]
public readonly record struct PlanChangeResponseDataSubscriptionChangeAction : IStringEnum
{
    public static readonly PlanChangeResponseDataSubscriptionChangeAction Downgrade = new(
        Values.Downgrade
    );

    public static readonly PlanChangeResponseDataSubscriptionChangeAction Invalid = new(
        Values.Invalid
    );

    public static readonly PlanChangeResponseDataSubscriptionChangeAction Subscribe = new(
        Values.Subscribe
    );

    public static readonly PlanChangeResponseDataSubscriptionChangeAction Unsubscribe = new(
        Values.Unsubscribe
    );

    public static readonly PlanChangeResponseDataSubscriptionChangeAction Upgrade = new(
        Values.Upgrade
    );

    public static readonly PlanChangeResponseDataSubscriptionChangeAction UpgradeTrial = new(
        Values.UpgradeTrial
    );

    public PlanChangeResponseDataSubscriptionChangeAction(string value)
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
    public static PlanChangeResponseDataSubscriptionChangeAction FromCustom(string value)
    {
        return new PlanChangeResponseDataSubscriptionChangeAction(value);
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
        PlanChangeResponseDataSubscriptionChangeAction value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        PlanChangeResponseDataSubscriptionChangeAction value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeResponseDataSubscriptionChangeAction value) =>
        value.Value;

    public static explicit operator PlanChangeResponseDataSubscriptionChangeAction(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Downgrade = "downgrade";

        public const string Invalid = "invalid";

        public const string Subscribe = "subscribe";

        public const string Unsubscribe = "unsubscribe";

        public const string Upgrade = "upgrade";

        public const string UpgradeTrial = "upgrade_trial";
    }
}
