using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingProductPricingUsageType>))]
[Serializable]
public readonly record struct BillingProductPricingUsageType : IStringEnum
{
    public static readonly BillingProductPricingUsageType Licensed = new(Values.Licensed);

    public static readonly BillingProductPricingUsageType Metered = new(Values.Metered);

    public BillingProductPricingUsageType(string value)
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
    public static BillingProductPricingUsageType FromCustom(string value)
    {
        return new BillingProductPricingUsageType(value);
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

    public static bool operator ==(BillingProductPricingUsageType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingProductPricingUsageType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingProductPricingUsageType value) => value.Value;

    public static explicit operator BillingProductPricingUsageType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Licensed = "licensed";

        public const string Metered = "metered";
    }
}
