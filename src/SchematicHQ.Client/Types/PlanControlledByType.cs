using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanControlledByType>))]
[Serializable]
public readonly record struct PlanControlledByType : IStringEnum
{
    public static readonly PlanControlledByType Schematic = new(Values.Schematic);

    public static readonly PlanControlledByType Stripe = new(Values.Stripe);

    public PlanControlledByType(string value)
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
    public static PlanControlledByType FromCustom(string value)
    {
        return new PlanControlledByType(value);
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

    public static bool operator ==(PlanControlledByType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanControlledByType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanControlledByType value) => value.Value;

    public static explicit operator PlanControlledByType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Schematic = "schematic";

        public const string Stripe = "stripe";
    }
}
