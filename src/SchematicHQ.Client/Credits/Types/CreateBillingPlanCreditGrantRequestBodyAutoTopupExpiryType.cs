using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType>)
)]
[Serializable]
public readonly record struct CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType
    : IStringEnum
{
    public static readonly CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType Duration =
        new(Values.Duration);

    public static readonly CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType NoExpiry =
        new(Values.NoExpiry);

    public static readonly CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType EndOfTrial =
        new(Values.EndOfTrial);

    public CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType(string value)
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
    public static CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType FromCustom(
        string value
    )
    {
        return new CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType(value);
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
        CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType value
    ) => value.Value;

    public static explicit operator CreateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Duration = "duration";

        public const string NoExpiry = "no_expiry";

        public const string EndOfTrial = "end_of_trial";
    }
}
