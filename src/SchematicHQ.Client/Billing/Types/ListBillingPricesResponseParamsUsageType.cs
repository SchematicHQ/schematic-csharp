using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListBillingPricesResponseParamsUsageType>))]
[Serializable]
public readonly record struct ListBillingPricesResponseParamsUsageType : IStringEnum
{
    public static readonly ListBillingPricesResponseParamsUsageType Licensed = new(Values.Licensed);

    public static readonly ListBillingPricesResponseParamsUsageType Metered = new(Values.Metered);

    public ListBillingPricesResponseParamsUsageType(string value)
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
    public static ListBillingPricesResponseParamsUsageType FromCustom(string value)
    {
        return new ListBillingPricesResponseParamsUsageType(value);
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
        ListBillingPricesResponseParamsUsageType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBillingPricesResponseParamsUsageType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBillingPricesResponseParamsUsageType value) =>
        value.Value;

    public static explicit operator ListBillingPricesResponseParamsUsageType(string value) =>
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
