using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListBillingProductsRequestPriceUsageType>))]
[Serializable]
public readonly record struct ListBillingProductsRequestPriceUsageType : IStringEnum
{
    public static readonly ListBillingProductsRequestPriceUsageType Licensed = new(Values.Licensed);

    public static readonly ListBillingProductsRequestPriceUsageType Metered = new(Values.Metered);

    public ListBillingProductsRequestPriceUsageType(string value)
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
    public static ListBillingProductsRequestPriceUsageType FromCustom(string value)
    {
        return new ListBillingProductsRequestPriceUsageType(value);
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
        ListBillingProductsRequestPriceUsageType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBillingProductsRequestPriceUsageType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBillingProductsRequestPriceUsageType value) =>
        value.Value;

    public static explicit operator ListBillingProductsRequestPriceUsageType(string value) =>
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
