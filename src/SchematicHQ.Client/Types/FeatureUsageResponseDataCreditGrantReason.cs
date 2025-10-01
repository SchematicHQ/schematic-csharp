using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureUsageResponseDataCreditGrantReason>))]
[Serializable]
public readonly record struct FeatureUsageResponseDataCreditGrantReason : IStringEnum
{
    public static readonly FeatureUsageResponseDataCreditGrantReason Free = new(Values.Free);

    public static readonly FeatureUsageResponseDataCreditGrantReason Plan = new(Values.Plan);

    public static readonly FeatureUsageResponseDataCreditGrantReason Purchased = new(
        Values.Purchased
    );

    public FeatureUsageResponseDataCreditGrantReason(string value)
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
    public static FeatureUsageResponseDataCreditGrantReason FromCustom(string value)
    {
        return new FeatureUsageResponseDataCreditGrantReason(value);
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
        FeatureUsageResponseDataCreditGrantReason value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        FeatureUsageResponseDataCreditGrantReason value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(FeatureUsageResponseDataCreditGrantReason value) =>
        value.Value;

    public static explicit operator FeatureUsageResponseDataCreditGrantReason(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Free = "free";

        public const string Plan = "plan";

        public const string Purchased = "purchased";
    }
}
