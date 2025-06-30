using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateFeatureRequestBodyLifecyclePhase>))]
[Serializable]
public readonly record struct UpdateFeatureRequestBodyLifecyclePhase : IStringEnum
{
    public static readonly UpdateFeatureRequestBodyLifecyclePhase AddOn = new(Values.AddOn);

    public static readonly UpdateFeatureRequestBodyLifecyclePhase Alpha = new(Values.Alpha);

    public static readonly UpdateFeatureRequestBodyLifecyclePhase Beta = new(Values.Beta);

    public static readonly UpdateFeatureRequestBodyLifecyclePhase Deprecated = new(
        Values.Deprecated
    );

    public static readonly UpdateFeatureRequestBodyLifecyclePhase Ga = new(Values.Ga);

    public static readonly UpdateFeatureRequestBodyLifecyclePhase InPlan = new(Values.InPlan);

    public static readonly UpdateFeatureRequestBodyLifecyclePhase Inactive = new(Values.Inactive);

    public static readonly UpdateFeatureRequestBodyLifecyclePhase InternalTesting = new(
        Values.InternalTesting
    );

    public static readonly UpdateFeatureRequestBodyLifecyclePhase Legacy = new(Values.Legacy);

    public UpdateFeatureRequestBodyLifecyclePhase(string value)
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
    public static UpdateFeatureRequestBodyLifecyclePhase FromCustom(string value)
    {
        return new UpdateFeatureRequestBodyLifecyclePhase(value);
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

    public static bool operator ==(UpdateFeatureRequestBodyLifecyclePhase value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UpdateFeatureRequestBodyLifecyclePhase value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UpdateFeatureRequestBodyLifecyclePhase value) =>
        value.Value;

    public static explicit operator UpdateFeatureRequestBodyLifecyclePhase(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AddOn = "add_on";

        public const string Alpha = "alpha";

        public const string Beta = "beta";

        public const string Deprecated = "deprecated";

        public const string Ga = "ga";

        public const string InPlan = "in_plan";

        public const string Inactive = "inactive";

        public const string InternalTesting = "internal_testing";

        public const string Legacy = "legacy";
    }
}
