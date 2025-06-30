using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateComponentRequestBodyEntityType>))]
[Serializable]
public readonly record struct UpdateComponentRequestBodyEntityType : IStringEnum
{
    public static readonly UpdateComponentRequestBodyEntityType Entitlement = new(
        Values.Entitlement
    );

    public static readonly UpdateComponentRequestBodyEntityType Billing = new(Values.Billing);

    public UpdateComponentRequestBodyEntityType(string value)
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
    public static UpdateComponentRequestBodyEntityType FromCustom(string value)
    {
        return new UpdateComponentRequestBodyEntityType(value);
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

    public static bool operator ==(UpdateComponentRequestBodyEntityType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UpdateComponentRequestBodyEntityType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UpdateComponentRequestBodyEntityType value) =>
        value.Value;

    public static explicit operator UpdateComponentRequestBodyEntityType(string value) =>
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
