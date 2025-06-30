using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountBillingProductsResponseParamsPriceUsageType>))]
[Serializable]
public readonly record struct CountBillingProductsResponseParamsPriceUsageType : IStringEnum
{
    public static readonly CountBillingProductsResponseParamsPriceUsageType Licensed = new(
        Values.Licensed
    );

    public static readonly CountBillingProductsResponseParamsPriceUsageType Metered = new(
        Values.Metered
    );

    public CountBillingProductsResponseParamsPriceUsageType(string value)
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
    public static CountBillingProductsResponseParamsPriceUsageType FromCustom(string value)
    {
        return new CountBillingProductsResponseParamsPriceUsageType(value);
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
        CountBillingProductsResponseParamsPriceUsageType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountBillingProductsResponseParamsPriceUsageType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CountBillingProductsResponseParamsPriceUsageType value
    ) => value.Value;

    public static explicit operator CountBillingProductsResponseParamsPriceUsageType(
        string value
    ) => new(value);

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
