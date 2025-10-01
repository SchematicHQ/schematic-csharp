using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateCompanyCreditGrantExpiryType>))]
[Serializable]
public readonly record struct CreateCompanyCreditGrantExpiryType : IStringEnum
{
    public static readonly CreateCompanyCreditGrantExpiryType Duration = new(Values.Duration);

    public static readonly CreateCompanyCreditGrantExpiryType NoExpiry = new(Values.NoExpiry);

    public static readonly CreateCompanyCreditGrantExpiryType EndOfTrial = new(Values.EndOfTrial);

    public static readonly CreateCompanyCreditGrantExpiryType EndOfBillingPeriod = new(
        Values.EndOfBillingPeriod
    );

    public static readonly CreateCompanyCreditGrantExpiryType EndOfNextBillingPeriod = new(
        Values.EndOfNextBillingPeriod
    );

    public CreateCompanyCreditGrantExpiryType(string value)
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
    public static CreateCompanyCreditGrantExpiryType FromCustom(string value)
    {
        return new CreateCompanyCreditGrantExpiryType(value);
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

    public static bool operator ==(CreateCompanyCreditGrantExpiryType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateCompanyCreditGrantExpiryType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateCompanyCreditGrantExpiryType value) => value.Value;

    public static explicit operator CreateCompanyCreditGrantExpiryType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Duration = "duration";

        public const string NoExpiry = "no_expiry";

        public const string EndOfTrial = "end_of_trial";

        public const string EndOfBillingPeriod = "end_of_billing_period";

        public const string EndOfNextBillingPeriod = "end_of_next_billing_period";
    }
}
