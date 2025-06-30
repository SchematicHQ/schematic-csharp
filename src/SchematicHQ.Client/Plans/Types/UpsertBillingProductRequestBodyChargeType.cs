using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpsertBillingProductRequestBodyChargeType>))]
[Serializable]
public readonly record struct UpsertBillingProductRequestBodyChargeType : IStringEnum
{
    public static readonly UpsertBillingProductRequestBodyChargeType OneTime = new(Values.OneTime);

    public static readonly UpsertBillingProductRequestBodyChargeType Recurring = new(
        Values.Recurring
    );

    public static readonly UpsertBillingProductRequestBodyChargeType Free = new(Values.Free);

    public UpsertBillingProductRequestBodyChargeType(string value)
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
    public static UpsertBillingProductRequestBodyChargeType FromCustom(string value)
    {
        return new UpsertBillingProductRequestBodyChargeType(value);
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
        UpsertBillingProductRequestBodyChargeType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpsertBillingProductRequestBodyChargeType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpsertBillingProductRequestBodyChargeType value) =>
        value.Value;

    public static explicit operator UpsertBillingProductRequestBodyChargeType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string OneTime = "one_time";

        public const string Recurring = "recurring";

        public const string Free = "free";
    }
}
