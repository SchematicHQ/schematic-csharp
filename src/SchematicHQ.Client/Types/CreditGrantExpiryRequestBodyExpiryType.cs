using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreditGrantExpiryRequestBodyExpiryType>))]
[Serializable]
public readonly record struct CreditGrantExpiryRequestBodyExpiryType : IStringEnum
{
    public static readonly CreditGrantExpiryRequestBodyExpiryType Duration = new(Values.Duration);

    public static readonly CreditGrantExpiryRequestBodyExpiryType NoExpiry = new(Values.NoExpiry);

    public static readonly CreditGrantExpiryRequestBodyExpiryType EndOfTrial = new(
        Values.EndOfTrial
    );

    public static readonly CreditGrantExpiryRequestBodyExpiryType EndOfBillingPeriod = new(
        Values.EndOfBillingPeriod
    );

    public static readonly CreditGrantExpiryRequestBodyExpiryType EndOfNextBillingPeriod = new(
        Values.EndOfNextBillingPeriod
    );

    public CreditGrantExpiryRequestBodyExpiryType(string value)
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
    public static CreditGrantExpiryRequestBodyExpiryType FromCustom(string value)
    {
        return new CreditGrantExpiryRequestBodyExpiryType(value);
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

    public static bool operator ==(CreditGrantExpiryRequestBodyExpiryType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditGrantExpiryRequestBodyExpiryType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditGrantExpiryRequestBodyExpiryType value) =>
        value.Value;

    public static explicit operator CreditGrantExpiryRequestBodyExpiryType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Duration = "duration";

        public const string NoExpiry = "no_expiry";

        public const string EndOfTrial = "end_of_trial";

        public const string EndOfBillingPeriod = "end_of_billing_period";

        public const string EndOfNextBillingPeriod = "end_of_next_billing_period";
    }
}
