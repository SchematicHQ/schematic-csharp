using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateCreditBundleDetailsRequestBodyExpiryType>))]
[Serializable]
public readonly record struct UpdateCreditBundleDetailsRequestBodyExpiryType : IStringEnum
{
    public static readonly UpdateCreditBundleDetailsRequestBodyExpiryType DaysFromPurchase = new(
        Values.DaysFromPurchase
    );

    public static readonly UpdateCreditBundleDetailsRequestBodyExpiryType NoExpiry = new(
        Values.NoExpiry
    );

    public UpdateCreditBundleDetailsRequestBodyExpiryType(string value)
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
    public static UpdateCreditBundleDetailsRequestBodyExpiryType FromCustom(string value)
    {
        return new UpdateCreditBundleDetailsRequestBodyExpiryType(value);
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
        UpdateCreditBundleDetailsRequestBodyExpiryType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateCreditBundleDetailsRequestBodyExpiryType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateCreditBundleDetailsRequestBodyExpiryType value) =>
        value.Value;

    public static explicit operator UpdateCreditBundleDetailsRequestBodyExpiryType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string DaysFromPurchase = "days_from_purchase";

        public const string NoExpiry = "no_expiry";
    }
}
