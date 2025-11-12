using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType>)
)]
[Serializable]
public readonly record struct UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType
    : IStringEnum
{
    public static readonly UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType Duration =
        new(Values.Duration);

    public static readonly UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType NoExpiry =
        new(Values.NoExpiry);

    public static readonly UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType EndOfTrial =
        new(Values.EndOfTrial);

    public UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType(string value)
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
    public static UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType FromCustom(
        string value
    )
    {
        return new UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType(value);
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
        UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType value
    ) => value.Value;

    public static explicit operator UpdateBillingPlanCreditGrantRequestBodyAutoTopupExpiryType(
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
