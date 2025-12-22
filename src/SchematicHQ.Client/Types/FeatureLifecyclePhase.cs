using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<FeatureLifecyclePhase>))]
[Serializable]
public readonly record struct FeatureLifecyclePhase : IStringEnum
{
    public static readonly FeatureLifecyclePhase AddOn = new(Values.AddOn);

    public static readonly FeatureLifecyclePhase Alpha = new(Values.Alpha);

    public static readonly FeatureLifecyclePhase Beta = new(Values.Beta);

    public static readonly FeatureLifecyclePhase Deprecated = new(Values.Deprecated);

    public static readonly FeatureLifecyclePhase Ga = new(Values.Ga);

    public static readonly FeatureLifecyclePhase Inactive = new(Values.Inactive);

    public static readonly FeatureLifecyclePhase InPlan = new(Values.InPlan);

    public static readonly FeatureLifecyclePhase InternalTesting = new(Values.InternalTesting);

    public static readonly FeatureLifecyclePhase Legacy = new(Values.Legacy);

    public FeatureLifecyclePhase(string value)
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
    public static FeatureLifecyclePhase FromCustom(string value)
    {
        return new FeatureLifecyclePhase(value);
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

    public static bool operator ==(FeatureLifecyclePhase value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FeatureLifecyclePhase value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FeatureLifecyclePhase value) => value.Value;

    public static explicit operator FeatureLifecyclePhase(string value) => new(value);

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

        public const string Inactive = "inactive";

        public const string InPlan = "in_plan";

        public const string InternalTesting = "internal_testing";

        public const string Legacy = "legacy";
    }
}
