using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingCreditRolloverPolicy>))]
[Serializable]
public readonly record struct BillingCreditRolloverPolicy : IStringEnum
{
    public static readonly BillingCreditRolloverPolicy Expire = new(Values.Expire);

    public static readonly BillingCreditRolloverPolicy None = new(Values.None);

    public static readonly BillingCreditRolloverPolicy Rollover = new(Values.Rollover);

    public BillingCreditRolloverPolicy(string value)
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
    public static BillingCreditRolloverPolicy FromCustom(string value)
    {
        return new BillingCreditRolloverPolicy(value);
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

    public static bool operator ==(BillingCreditRolloverPolicy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingCreditRolloverPolicy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingCreditRolloverPolicy value) => value.Value;

    public static explicit operator BillingCreditRolloverPolicy(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Expire = "expire";

        public const string None = "none";

        public const string Rollover = "rollover";
    }
}
