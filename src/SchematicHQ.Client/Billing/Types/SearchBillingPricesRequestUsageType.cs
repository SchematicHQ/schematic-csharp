using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<SearchBillingPricesRequestUsageType>))]
[Serializable]
public readonly record struct SearchBillingPricesRequestUsageType : IStringEnum
{
    public static readonly SearchBillingPricesRequestUsageType Licensed = new(Values.Licensed);

    public static readonly SearchBillingPricesRequestUsageType Metered = new(Values.Metered);

    public SearchBillingPricesRequestUsageType(string value)
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
    public static SearchBillingPricesRequestUsageType FromCustom(string value)
    {
        return new SearchBillingPricesRequestUsageType(value);
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

    public static bool operator ==(SearchBillingPricesRequestUsageType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SearchBillingPricesRequestUsageType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SearchBillingPricesRequestUsageType value) =>
        value.Value;

    public static explicit operator SearchBillingPricesRequestUsageType(string value) => new(value);

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
