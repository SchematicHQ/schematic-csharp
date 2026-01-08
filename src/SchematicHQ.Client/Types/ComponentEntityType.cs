using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ComponentEntityType>))]
[Serializable]
public readonly record struct ComponentEntityType : IStringEnum
{
    public static readonly ComponentEntityType Billing = new(Values.Billing);

    public static readonly ComponentEntityType Entitlement = new(Values.Entitlement);

    public ComponentEntityType(string value)
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
    public static ComponentEntityType FromCustom(string value)
    {
        return new ComponentEntityType(value);
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

    public static bool operator ==(ComponentEntityType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ComponentEntityType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ComponentEntityType value) => value.Value;

    public static explicit operator ComponentEntityType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Billing = "billing";

        public const string Entitlement = "entitlement";
    }
}
