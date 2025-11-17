using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListBillingPricesRequestTiersMode>))]
[Serializable]
public readonly record struct ListBillingPricesRequestTiersMode : IStringEnum
{
    public static readonly ListBillingPricesRequestTiersMode Volume = new(Values.Volume);

    public static readonly ListBillingPricesRequestTiersMode Graduated = new(Values.Graduated);

    public ListBillingPricesRequestTiersMode(string value)
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
    public static ListBillingPricesRequestTiersMode FromCustom(string value)
    {
        return new ListBillingPricesRequestTiersMode(value);
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

    public static bool operator ==(ListBillingPricesRequestTiersMode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListBillingPricesRequestTiersMode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListBillingPricesRequestTiersMode value) => value.Value;

    public static explicit operator ListBillingPricesRequestTiersMode(string value) => new(value);

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
