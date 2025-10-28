using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeResponseDataBasePlanAction>))]
[Serializable]
public readonly record struct PlanChangeResponseDataBasePlanAction : IStringEnum
{
    public static readonly PlanChangeResponseDataBasePlanAction Fallback = new(Values.Fallback);

    public static readonly PlanChangeResponseDataBasePlanAction Initial = new(Values.Initial);

    public static readonly PlanChangeResponseDataBasePlanAction Trait = new(Values.Trait);

    public static readonly PlanChangeResponseDataBasePlanAction TrialExpiry = new(
        Values.TrialExpiry
    );

    public PlanChangeResponseDataBasePlanAction(string value)
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
    public static PlanChangeResponseDataBasePlanAction FromCustom(string value)
    {
        return new PlanChangeResponseDataBasePlanAction(value);
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

    public static bool operator ==(PlanChangeResponseDataBasePlanAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeResponseDataBasePlanAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeResponseDataBasePlanAction value) =>
        value.Value;

    public static explicit operator PlanChangeResponseDataBasePlanAction(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Fallback = "fallback";

        public const string Initial = "initial";

        public const string Trait = "trait";

        public const string TrialExpiry = "trial_expiry";
    }
}
