using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingCreditExpiryUnit>))]
[Serializable]
public readonly record struct BillingCreditExpiryUnit : IStringEnum
{
    public static readonly BillingCreditExpiryUnit BillingPeriods = new(Values.BillingPeriods);

    public static readonly BillingCreditExpiryUnit Days = new(Values.Days);

    public BillingCreditExpiryUnit(string value)
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
    public static BillingCreditExpiryUnit FromCustom(string value)
    {
        return new BillingCreditExpiryUnit(value);
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

    public static bool operator ==(BillingCreditExpiryUnit value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditExpiryUnit value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditExpiryUnit value) => value.Value;

    public static explicit operator BillingCreditExpiryUnit(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string BillingPeriods = "billing_periods";

        public const string Days = "days";
    }
}
