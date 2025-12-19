using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingPriceScheme>))]
[Serializable]
public readonly record struct BillingPriceScheme : IStringEnum
{
    public static readonly BillingPriceScheme PerUnit = new(Values.PerUnit);

    public static readonly BillingPriceScheme Tiered = new(Values.Tiered);

    public BillingPriceScheme(string value)
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
    public static BillingPriceScheme FromCustom(string value)
    {
        return new BillingPriceScheme(value);
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

    public static bool operator ==(BillingPriceScheme value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingPriceScheme value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingPriceScheme value) => value.Value;

    public static explicit operator BillingPriceScheme(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PerUnit = "per_unit";

        public const string Tiered = "tiered";
    }
}
