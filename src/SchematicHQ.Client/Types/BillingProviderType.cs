using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingProviderType>))]
[Serializable]
public readonly record struct BillingProviderType : IStringEnum
{
    public static readonly BillingProviderType Schematic = new(Values.Schematic);

    public static readonly BillingProviderType Stripe = new(Values.Stripe);

    public BillingProviderType(string value)
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
    public static BillingProviderType FromCustom(string value)
    {
        return new BillingProviderType(value);
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

    public static bool operator ==(BillingProviderType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingProviderType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingProviderType value) => value.Value;

    public static explicit operator BillingProviderType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Schematic = "schematic";

        public const string Stripe = "stripe";
    }
}
