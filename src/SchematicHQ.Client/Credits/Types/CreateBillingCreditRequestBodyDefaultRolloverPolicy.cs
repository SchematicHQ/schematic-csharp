using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateBillingCreditRequestBodyDefaultRolloverPolicy>))]
[Serializable]
public readonly record struct CreateBillingCreditRequestBodyDefaultRolloverPolicy : IStringEnum
{
    public static readonly CreateBillingCreditRequestBodyDefaultRolloverPolicy None = new(
        Values.None
    );

    public static readonly CreateBillingCreditRequestBodyDefaultRolloverPolicy Rollover = new(
        Values.Rollover
    );

    public static readonly CreateBillingCreditRequestBodyDefaultRolloverPolicy Expire = new(
        Values.Expire
    );

    public CreateBillingCreditRequestBodyDefaultRolloverPolicy(string value)
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
    public static CreateBillingCreditRequestBodyDefaultRolloverPolicy FromCustom(string value)
    {
        return new CreateBillingCreditRequestBodyDefaultRolloverPolicy(value);
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
        CreateBillingCreditRequestBodyDefaultRolloverPolicy value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingCreditRequestBodyDefaultRolloverPolicy value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingCreditRequestBodyDefaultRolloverPolicy value
    ) => value.Value;

    public static explicit operator CreateBillingCreditRequestBodyDefaultRolloverPolicy(
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
