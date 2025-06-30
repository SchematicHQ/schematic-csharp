using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountBillingProductsRequestPriceUsageType>))]
[Serializable]
public readonly record struct CountBillingProductsRequestPriceUsageType : IStringEnum
{
    public static readonly CountBillingProductsRequestPriceUsageType Licensed = new(
        Values.Licensed
    );

    public static readonly CountBillingProductsRequestPriceUsageType Metered = new(Values.Metered);

    public CountBillingProductsRequestPriceUsageType(string value)
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
    public static CountBillingProductsRequestPriceUsageType FromCustom(string value)
    {
        return new CountBillingProductsRequestPriceUsageType(value);
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
        CountBillingProductsRequestPriceUsageType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountBillingProductsRequestPriceUsageType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CountBillingProductsRequestPriceUsageType value) =>
        value.Value;

    public static explicit operator CountBillingProductsRequestPriceUsageType(string value) =>
        new(value);

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
