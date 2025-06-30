using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<SearchBillingPricesRequestTiersMode>))]
[Serializable]
public readonly record struct SearchBillingPricesRequestTiersMode : IStringEnum
{
    public static readonly SearchBillingPricesRequestTiersMode Volume = new(Values.Volume);

    public static readonly SearchBillingPricesRequestTiersMode Graduated = new(Values.Graduated);

    public SearchBillingPricesRequestTiersMode(string value)
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
    public static SearchBillingPricesRequestTiersMode FromCustom(string value)
    {
        return new SearchBillingPricesRequestTiersMode(value);
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

    public static bool operator ==(SearchBillingPricesRequestTiersMode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SearchBillingPricesRequestTiersMode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SearchBillingPricesRequestTiersMode value) =>
        value.Value;

    public static explicit operator SearchBillingPricesRequestTiersMode(string value) => new(value);

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
