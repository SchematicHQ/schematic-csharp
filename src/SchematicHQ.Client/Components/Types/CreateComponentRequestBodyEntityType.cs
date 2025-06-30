using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateComponentRequestBodyEntityType>))]
[Serializable]
public readonly record struct CreateComponentRequestBodyEntityType : IStringEnum
{
    public static readonly CreateComponentRequestBodyEntityType Entitlement = new(
        Values.Entitlement
    );

    public static readonly CreateComponentRequestBodyEntityType Billing = new(Values.Billing);

    public CreateComponentRequestBodyEntityType(string value)
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
    public static CreateComponentRequestBodyEntityType FromCustom(string value)
    {
        return new CreateComponentRequestBodyEntityType(value);
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

    public static bool operator ==(CreateComponentRequestBodyEntityType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateComponentRequestBodyEntityType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateComponentRequestBodyEntityType value) =>
        value.Value;

    public static explicit operator CreateComponentRequestBodyEntityType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Entitlement = "entitlement";

        public const string Billing = "billing";
    }
}
