using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<EntitlementPriceBehavior>))]
[Serializable]
public readonly record struct EntitlementPriceBehavior : IStringEnum
{
    public static readonly EntitlementPriceBehavior CreditBurndown = new(Values.CreditBurndown);

    public static readonly EntitlementPriceBehavior Overage = new(Values.Overage);

    public static readonly EntitlementPriceBehavior PayAsYouGo = new(Values.PayAsYouGo);

    public static readonly EntitlementPriceBehavior PayInAdvance = new(Values.PayInAdvance);

    public static readonly EntitlementPriceBehavior Tier = new(Values.Tier);

    public EntitlementPriceBehavior(string value)
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
    public static EntitlementPriceBehavior FromCustom(string value)
    {
        return new EntitlementPriceBehavior(value);
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

    public static bool operator ==(EntitlementPriceBehavior value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EntitlementPriceBehavior value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EntitlementPriceBehavior value) => value.Value;

    public static explicit operator EntitlementPriceBehavior(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CreditBurndown = "credit_burndown";

        public const string Overage = "overage";

        public const string PayAsYouGo = "pay_as_you_go";

        public const string PayInAdvance = "pay_in_advance";

        public const string Tier = "tier";
    }
}
