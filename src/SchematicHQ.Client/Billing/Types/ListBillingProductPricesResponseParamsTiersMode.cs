using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListBillingProductPricesResponseParamsTiersMode>))]
[Serializable]
public readonly record struct ListBillingProductPricesResponseParamsTiersMode : IStringEnum
{
    public static readonly ListBillingProductPricesResponseParamsTiersMode Volume = new(
        Values.Volume
    );

    public static readonly ListBillingProductPricesResponseParamsTiersMode Graduated = new(
        Values.Graduated
    );

    public ListBillingProductPricesResponseParamsTiersMode(string value)
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
    public static ListBillingProductPricesResponseParamsTiersMode FromCustom(string value)
    {
        return new ListBillingProductPricesResponseParamsTiersMode(value);
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
        ListBillingProductPricesResponseParamsTiersMode value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBillingProductPricesResponseParamsTiersMode value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBillingProductPricesResponseParamsTiersMode value) =>
        value.Value;

    public static explicit operator ListBillingProductPricesResponseParamsTiersMode(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Volume = "volume";

        public const string Graduated = "graduated";
    }
}
