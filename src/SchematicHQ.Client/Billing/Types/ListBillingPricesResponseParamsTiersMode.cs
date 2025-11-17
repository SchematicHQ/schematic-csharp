using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListBillingPricesResponseParamsTiersMode>))]
[Serializable]
public readonly record struct ListBillingPricesResponseParamsTiersMode : IStringEnum
{
    public static readonly ListBillingPricesResponseParamsTiersMode Volume = new(Values.Volume);

    public static readonly ListBillingPricesResponseParamsTiersMode Graduated = new(
        Values.Graduated
    );

    public ListBillingPricesResponseParamsTiersMode(string value)
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
    public static ListBillingPricesResponseParamsTiersMode FromCustom(string value)
    {
        return new ListBillingPricesResponseParamsTiersMode(value);
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
        ListBillingPricesResponseParamsTiersMode value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBillingPricesResponseParamsTiersMode value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBillingPricesResponseParamsTiersMode value) =>
        value.Value;

    public static explicit operator ListBillingPricesResponseParamsTiersMode(string value) =>
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
