using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeSubscriptionAction>))]
[Serializable]
public readonly record struct PlanChangeSubscriptionAction : IStringEnum
{
    public static readonly PlanChangeSubscriptionAction Adjustment = new(Values.Adjustment);

    public static readonly PlanChangeSubscriptionAction Downgrade = new(Values.Downgrade);

    public static readonly PlanChangeSubscriptionAction Invalid = new(Values.Invalid);

    public static readonly PlanChangeSubscriptionAction Subscribe = new(Values.Subscribe);

    public static readonly PlanChangeSubscriptionAction Unsubscribe = new(Values.Unsubscribe);

    public static readonly PlanChangeSubscriptionAction Upgrade = new(Values.Upgrade);

    public static readonly PlanChangeSubscriptionAction UpgradeTrial = new(Values.UpgradeTrial);

    public PlanChangeSubscriptionAction(string value)
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
    public static PlanChangeSubscriptionAction FromCustom(string value)
    {
        return new PlanChangeSubscriptionAction(value);
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

    public static bool operator ==(PlanChangeSubscriptionAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeSubscriptionAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeSubscriptionAction value) => value.Value;

    public static explicit operator PlanChangeSubscriptionAction(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Adjustment = "adjustment";

        public const string Downgrade = "downgrade";

        public const string Invalid = "invalid";

        public const string Subscribe = "subscribe";

        public const string Unsubscribe = "unsubscribe";

        public const string Upgrade = "upgrade";

        public const string UpgradeTrial = "upgrade_trial";
    }
}
