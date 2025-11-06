using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreditGrantExpiryRequestBodyResetCadence>))]
[Serializable]
public readonly record struct CreditGrantExpiryRequestBodyResetCadence : IStringEnum
{
    public static readonly CreditGrantExpiryRequestBodyResetCadence Monthly = new(Values.Monthly);

    public static readonly CreditGrantExpiryRequestBodyResetCadence Yearly = new(Values.Yearly);

    public static readonly CreditGrantExpiryRequestBodyResetCadence Daily = new(Values.Daily);

    public static readonly CreditGrantExpiryRequestBodyResetCadence Weekly = new(Values.Weekly);

    public CreditGrantExpiryRequestBodyResetCadence(string value)
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
    public static CreditGrantExpiryRequestBodyResetCadence FromCustom(string value)
    {
        return new CreditGrantExpiryRequestBodyResetCadence(value);
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
        CreditGrantExpiryRequestBodyResetCadence value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreditGrantExpiryRequestBodyResetCadence value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreditGrantExpiryRequestBodyResetCadence value) =>
        value.Value;

    public static explicit operator CreditGrantExpiryRequestBodyResetCadence(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Monthly = "monthly";

        public const string Yearly = "yearly";

        public const string Daily = "daily";

        public const string Weekly = "weekly";
    }
}
