using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListBillingProductPricesRequestTiersMode>))]
[Serializable]
public readonly record struct ListBillingProductPricesRequestTiersMode : IStringEnum
{
    public static readonly ListBillingProductPricesRequestTiersMode Volume = new(Values.Volume);

    public static readonly ListBillingProductPricesRequestTiersMode Graduated = new(
        Values.Graduated
    );

    public ListBillingProductPricesRequestTiersMode(string value)
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
    public static ListBillingProductPricesRequestTiersMode FromCustom(string value)
    {
        return new ListBillingProductPricesRequestTiersMode(value);
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
        ListBillingProductPricesRequestTiersMode value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBillingProductPricesRequestTiersMode value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBillingProductPricesRequestTiersMode value) =>
        value.Value;

    public static explicit operator ListBillingProductPricesRequestTiersMode(string value) =>
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
