using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListProductPricesRequestPriceUsageType>))]
[Serializable]
public readonly record struct ListProductPricesRequestPriceUsageType : IStringEnum
{
    public static readonly ListProductPricesRequestPriceUsageType Licensed = new(Values.Licensed);

    public static readonly ListProductPricesRequestPriceUsageType Metered = new(Values.Metered);

    public ListProductPricesRequestPriceUsageType(string value)
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
    public static ListProductPricesRequestPriceUsageType FromCustom(string value)
    {
        return new ListProductPricesRequestPriceUsageType(value);
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

    public static bool operator ==(ListProductPricesRequestPriceUsageType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListProductPricesRequestPriceUsageType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListProductPricesRequestPriceUsageType value) =>
        value.Value;

    public static explicit operator ListProductPricesRequestPriceUsageType(string value) =>
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
