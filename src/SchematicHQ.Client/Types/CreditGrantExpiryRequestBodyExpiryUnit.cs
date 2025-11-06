using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreditGrantExpiryRequestBodyExpiryUnit>))]
[Serializable]
public readonly record struct CreditGrantExpiryRequestBodyExpiryUnit : IStringEnum
{
    public static readonly CreditGrantExpiryRequestBodyExpiryUnit Days = new(Values.Days);

    public static readonly CreditGrantExpiryRequestBodyExpiryUnit BillingPeriods = new(
        Values.BillingPeriods
    );

    public CreditGrantExpiryRequestBodyExpiryUnit(string value)
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
    public static CreditGrantExpiryRequestBodyExpiryUnit FromCustom(string value)
    {
        return new CreditGrantExpiryRequestBodyExpiryUnit(value);
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

    public static bool operator ==(CreditGrantExpiryRequestBodyExpiryUnit value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditGrantExpiryRequestBodyExpiryUnit value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditGrantExpiryRequestBodyExpiryUnit value) =>
        value.Value;

    public static explicit operator CreditGrantExpiryRequestBodyExpiryUnit(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Days = "days";

        public const string BillingPeriods = "billing_periods";
    }
}
