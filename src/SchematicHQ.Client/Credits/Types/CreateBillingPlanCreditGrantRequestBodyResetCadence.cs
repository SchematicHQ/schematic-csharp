using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateBillingPlanCreditGrantRequestBodyResetCadence>))]
[Serializable]
public readonly record struct CreateBillingPlanCreditGrantRequestBodyResetCadence : IStringEnum
{
    public static readonly CreateBillingPlanCreditGrantRequestBodyResetCadence Monthly = new(
        Values.Monthly
    );

    public static readonly CreateBillingPlanCreditGrantRequestBodyResetCadence Yearly = new(
        Values.Yearly
    );

    public static readonly CreateBillingPlanCreditGrantRequestBodyResetCadence Daily = new(
        Values.Daily
    );

    public static readonly CreateBillingPlanCreditGrantRequestBodyResetCadence Weekly = new(
        Values.Weekly
    );

    public CreateBillingPlanCreditGrantRequestBodyResetCadence(string value)
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
    public static CreateBillingPlanCreditGrantRequestBodyResetCadence FromCustom(string value)
    {
        return new CreateBillingPlanCreditGrantRequestBodyResetCadence(value);
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
        CreateBillingPlanCreditGrantRequestBodyResetCadence value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingPlanCreditGrantRequestBodyResetCadence value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingPlanCreditGrantRequestBodyResetCadence value
    ) => value.Value;

    public static explicit operator CreateBillingPlanCreditGrantRequestBodyResetCadence(
        string value
    ) => new(value);

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
