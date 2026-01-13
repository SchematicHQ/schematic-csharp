using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<BillingTiersMode>))]
[Serializable]
public readonly record struct BillingTiersMode : IStringEnum
{
    public static readonly BillingTiersMode Graduated = new(Values.Graduated);

    public static readonly BillingTiersMode Volume = new(Values.Volume);

    public BillingTiersMode(string value)
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
    public static BillingTiersMode FromCustom(string value)
    {
        return new BillingTiersMode(value);
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

    public static bool operator ==(BillingTiersMode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BillingTiersMode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BillingTiersMode value) => value.Value;

    public static explicit operator BillingTiersMode(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Graduated = "graduated";

        public const string Volume = "volume";
    }
}
