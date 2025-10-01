using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateCompanyCreditGrantExpiryUnit>))]
[Serializable]
public readonly record struct CreateCompanyCreditGrantExpiryUnit : IStringEnum
{
    public static readonly CreateCompanyCreditGrantExpiryUnit Days = new(Values.Days);

    public static readonly CreateCompanyCreditGrantExpiryUnit BillingPeriods = new(
        Values.BillingPeriods
    );

    public CreateCompanyCreditGrantExpiryUnit(string value)
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
    public static CreateCompanyCreditGrantExpiryUnit FromCustom(string value)
    {
        return new CreateCompanyCreditGrantExpiryUnit(value);
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

    public static bool operator ==(CreateCompanyCreditGrantExpiryUnit value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateCompanyCreditGrantExpiryUnit value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateCompanyCreditGrantExpiryUnit value) => value.Value;

    public static explicit operator CreateCompanyCreditGrantExpiryUnit(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Days = "days";

        public const string BillingPeriods = "billing_periods";
    }
}
