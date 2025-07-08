using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UsageBasedEntitlementRequestBodyPriceBehavior>))]
[Serializable]
public readonly record struct UsageBasedEntitlementRequestBodyPriceBehavior : IStringEnum
{
    public static readonly UsageBasedEntitlementRequestBodyPriceBehavior PayAsYouGo = new(
        Values.PayAsYouGo
    );

    public static readonly UsageBasedEntitlementRequestBodyPriceBehavior PayInAdvance = new(
        Values.PayInAdvance
    );

    public static readonly UsageBasedEntitlementRequestBodyPriceBehavior Overage = new(
        Values.Overage
    );

    public static readonly UsageBasedEntitlementRequestBodyPriceBehavior CreditBurndown = new(
        Values.CreditBurndown
    );

    public static readonly UsageBasedEntitlementRequestBodyPriceBehavior Tier = new(Values.Tier);

    public UsageBasedEntitlementRequestBodyPriceBehavior(string value)
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
    public static UsageBasedEntitlementRequestBodyPriceBehavior FromCustom(string value)
    {
        return new UsageBasedEntitlementRequestBodyPriceBehavior(value);
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
        UsageBasedEntitlementRequestBodyPriceBehavior value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UsageBasedEntitlementRequestBodyPriceBehavior value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UsageBasedEntitlementRequestBodyPriceBehavior value) =>
        value.Value;

    public static explicit operator UsageBasedEntitlementRequestBodyPriceBehavior(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PayAsYouGo = "pay_as_you_go";

        public const string PayInAdvance = "pay_in_advance";

        public const string Overage = "overage";

        public const string CreditBurndown = "credit_burndown";

        public const string Tier = "tier";
    }
}
