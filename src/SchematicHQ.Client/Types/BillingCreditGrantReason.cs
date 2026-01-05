using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingCreditGrantReason>))]
[Serializable]
public readonly record struct BillingCreditGrantReason : IStringEnum
{
    public static readonly BillingCreditGrantReason BillingCreditAutoTopup = new(
        Values.BillingCreditAutoTopup
    );

    public static readonly BillingCreditGrantReason Free = new(Values.Free);

    public static readonly BillingCreditGrantReason Plan = new(Values.Plan);

    public static readonly BillingCreditGrantReason Purchased = new(Values.Purchased);

    public BillingCreditGrantReason(string value)
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
    public static BillingCreditGrantReason FromCustom(string value)
    {
        return new BillingCreditGrantReason(value);
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

    public static bool operator ==(BillingCreditGrantReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditGrantReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditGrantReason value) => value.Value;

    public static explicit operator BillingCreditGrantReason(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string BillingCreditAutoTopup = "billing_credit_auto_topup";

        public const string Free = "free";

        public const string Plan = "plan";

        public const string Purchased = "purchased";
    }
}
