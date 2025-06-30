using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateOrUpdateRuleRequestBodyRuleType>))]
[Serializable]
public readonly record struct CreateOrUpdateRuleRequestBodyRuleType : IStringEnum
{
    public static readonly CreateOrUpdateRuleRequestBodyRuleType GlobalOverride = new(
        Values.GlobalOverride
    );

    public static readonly CreateOrUpdateRuleRequestBodyRuleType CompanyOverride = new(
        Values.CompanyOverride
    );

    public static readonly CreateOrUpdateRuleRequestBodyRuleType PlanEntitlement = new(
        Values.PlanEntitlement
    );

    public static readonly CreateOrUpdateRuleRequestBodyRuleType Standard = new(Values.Standard);

    public static readonly CreateOrUpdateRuleRequestBodyRuleType Default = new(Values.Default);

    public static readonly CreateOrUpdateRuleRequestBodyRuleType PlanAudience = new(
        Values.PlanAudience
    );

    public CreateOrUpdateRuleRequestBodyRuleType(string value)
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
    public static CreateOrUpdateRuleRequestBodyRuleType FromCustom(string value)
    {
        return new CreateOrUpdateRuleRequestBodyRuleType(value);
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

    public static bool operator ==(CreateOrUpdateRuleRequestBodyRuleType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateOrUpdateRuleRequestBodyRuleType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateOrUpdateRuleRequestBodyRuleType value) =>
        value.Value;

    public static explicit operator CreateOrUpdateRuleRequestBodyRuleType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string GlobalOverride = "global_override";

        public const string CompanyOverride = "company_override";

        public const string PlanEntitlement = "plan_entitlement";

        public const string Standard = "standard";

        public const string Default = "default";

        public const string PlanAudience = "plan_audience";
    }
}
