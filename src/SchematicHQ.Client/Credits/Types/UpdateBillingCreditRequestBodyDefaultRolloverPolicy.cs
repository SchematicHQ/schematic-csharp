using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateBillingCreditRequestBodyDefaultRolloverPolicy>))]
[Serializable]
public readonly record struct UpdateBillingCreditRequestBodyDefaultRolloverPolicy : IStringEnum
{
    public static readonly UpdateBillingCreditRequestBodyDefaultRolloverPolicy None = new(
        Values.None
    );

    public static readonly UpdateBillingCreditRequestBodyDefaultRolloverPolicy Rollover = new(
        Values.Rollover
    );

    public static readonly UpdateBillingCreditRequestBodyDefaultRolloverPolicy Expire = new(
        Values.Expire
    );

    public UpdateBillingCreditRequestBodyDefaultRolloverPolicy(string value)
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
    public static UpdateBillingCreditRequestBodyDefaultRolloverPolicy FromCustom(string value)
    {
        return new UpdateBillingCreditRequestBodyDefaultRolloverPolicy(value);
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
        UpdateBillingCreditRequestBodyDefaultRolloverPolicy value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateBillingCreditRequestBodyDefaultRolloverPolicy value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        UpdateBillingCreditRequestBodyDefaultRolloverPolicy value
    ) => value.Value;

    public static explicit operator UpdateBillingCreditRequestBodyDefaultRolloverPolicy(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string None = "none";

        public const string Rollover = "rollover";

        public const string Expire = "expire";
    }
}
