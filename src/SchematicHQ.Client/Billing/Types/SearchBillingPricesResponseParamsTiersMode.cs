using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<SearchBillingPricesResponseParamsTiersMode>))]
[Serializable]
public readonly record struct SearchBillingPricesResponseParamsTiersMode : IStringEnum
{
    public static readonly SearchBillingPricesResponseParamsTiersMode Volume = new(Values.Volume);

    public static readonly SearchBillingPricesResponseParamsTiersMode Graduated = new(
        Values.Graduated
    );

    public SearchBillingPricesResponseParamsTiersMode(string value)
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
    public static SearchBillingPricesResponseParamsTiersMode FromCustom(string value)
    {
        return new SearchBillingPricesResponseParamsTiersMode(value);
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
        SearchBillingPricesResponseParamsTiersMode value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        SearchBillingPricesResponseParamsTiersMode value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(SearchBillingPricesResponseParamsTiersMode value) =>
        value.Value;

    public static explicit operator SearchBillingPricesResponseParamsTiersMode(string value) =>
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
