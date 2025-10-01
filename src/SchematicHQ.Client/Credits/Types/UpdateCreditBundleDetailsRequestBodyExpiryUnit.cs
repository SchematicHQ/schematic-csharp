using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateCreditBundleDetailsRequestBodyExpiryUnit>))]
[Serializable]
public readonly record struct UpdateCreditBundleDetailsRequestBodyExpiryUnit : IStringEnum
{
    public static readonly UpdateCreditBundleDetailsRequestBodyExpiryUnit Days = new(Values.Days);

    public static readonly UpdateCreditBundleDetailsRequestBodyExpiryUnit BillingPeriods = new(
        Values.BillingPeriods
    );

    public UpdateCreditBundleDetailsRequestBodyExpiryUnit(string value)
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
    public static UpdateCreditBundleDetailsRequestBodyExpiryUnit FromCustom(string value)
    {
        return new UpdateCreditBundleDetailsRequestBodyExpiryUnit(value);
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
        UpdateCreditBundleDetailsRequestBodyExpiryUnit value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateCreditBundleDetailsRequestBodyExpiryUnit value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateCreditBundleDetailsRequestBodyExpiryUnit value) =>
        value.Value;

    public static explicit operator UpdateCreditBundleDetailsRequestBodyExpiryUnit(string value) =>
        new(value);

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
