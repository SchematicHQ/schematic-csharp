using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreditGrantExpiryRequestBodyResetType>))]
[Serializable]
public readonly record struct CreditGrantExpiryRequestBodyResetType : IStringEnum
{
    public static readonly CreditGrantExpiryRequestBodyResetType PlanPeriod = new(
        Values.PlanPeriod
    );

    public static readonly CreditGrantExpiryRequestBodyResetType NoReset = new(Values.NoReset);

    public CreditGrantExpiryRequestBodyResetType(string value)
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
    public static CreditGrantExpiryRequestBodyResetType FromCustom(string value)
    {
        return new CreditGrantExpiryRequestBodyResetType(value);
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

    public static bool operator ==(CreditGrantExpiryRequestBodyResetType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditGrantExpiryRequestBodyResetType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditGrantExpiryRequestBodyResetType value) =>
        value.Value;

    public static explicit operator CreditGrantExpiryRequestBodyResetType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PlanPeriod = "plan_period";

        public const string NoReset = "no_reset";
    }
}
