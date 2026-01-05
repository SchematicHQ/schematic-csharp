using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<EntitlementType>))]
[Serializable]
public readonly record struct EntitlementType : IStringEnum
{
    public static readonly EntitlementType CompanyOverride = new(Values.CompanyOverride);

    public static readonly EntitlementType PlanEntitlement = new(Values.PlanEntitlement);

    public static readonly EntitlementType Unknown = new(Values.Unknown);

    public EntitlementType(string value)
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
    public static EntitlementType FromCustom(string value)
    {
        return new EntitlementType(value);
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

    public static bool operator ==(EntitlementType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EntitlementType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EntitlementType value) => value.Value;

    public static explicit operator EntitlementType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CompanyOverride = "company_override";

        public const string PlanEntitlement = "plan_entitlement";

        public const string Unknown = "unknown";
    }
}
