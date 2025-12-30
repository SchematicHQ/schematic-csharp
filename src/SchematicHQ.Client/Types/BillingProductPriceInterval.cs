using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingProductPriceInterval>))]
[Serializable]
public readonly record struct BillingProductPriceInterval : IStringEnum
{
    public static readonly BillingProductPriceInterval Day = new(Values.Day);

    public static readonly BillingProductPriceInterval Month = new(Values.Month);

    public static readonly BillingProductPriceInterval OneTime = new(Values.OneTime);

    public static readonly BillingProductPriceInterval Year = new(Values.Year);

    public BillingProductPriceInterval(string value)
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
    public static BillingProductPriceInterval FromCustom(string value)
    {
        return new BillingProductPriceInterval(value);
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

    public static bool operator ==(BillingProductPriceInterval value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingProductPriceInterval value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingProductPriceInterval value) => value.Value;

    public static explicit operator BillingProductPriceInterval(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Day = "day";

        public const string Month = "month";

        public const string OneTime = "one-time";

        public const string Year = "year";
    }
}
