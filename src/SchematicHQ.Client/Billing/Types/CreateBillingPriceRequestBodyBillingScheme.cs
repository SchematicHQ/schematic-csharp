using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateBillingPriceRequestBodyBillingScheme>))]
[Serializable]
public readonly record struct CreateBillingPriceRequestBodyBillingScheme : IStringEnum
{
    public static readonly CreateBillingPriceRequestBodyBillingScheme PerUnit = new(Values.PerUnit);

    public static readonly CreateBillingPriceRequestBodyBillingScheme Tiered = new(Values.Tiered);

    public CreateBillingPriceRequestBodyBillingScheme(string value)
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
    public static CreateBillingPriceRequestBodyBillingScheme FromCustom(string value)
    {
        return new CreateBillingPriceRequestBodyBillingScheme(value);
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
        CreateBillingPriceRequestBodyBillingScheme value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingPriceRequestBodyBillingScheme value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateBillingPriceRequestBodyBillingScheme value) =>
        value.Value;

    public static explicit operator CreateBillingPriceRequestBodyBillingScheme(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PerUnit = "per_unit";

        public const string Tiered = "tiered";
    }
}
