using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingPriceUsageType>))]
[Serializable]
public readonly record struct BillingPriceUsageType : IStringEnum
{
    public static readonly BillingPriceUsageType Licensed = new(Values.Licensed);

    public static readonly BillingPriceUsageType Metered = new(Values.Metered);

    public BillingPriceUsageType(string value)
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
    public static BillingPriceUsageType FromCustom(string value)
    {
        return new BillingPriceUsageType(value);
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

    public static bool operator ==(BillingPriceUsageType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingPriceUsageType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingPriceUsageType value) => value.Value;

    public static explicit operator BillingPriceUsageType(string value) => new(value);

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
