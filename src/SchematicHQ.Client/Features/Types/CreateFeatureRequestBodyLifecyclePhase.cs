using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateFeatureRequestBodyLifecyclePhase>))]
[Serializable]
public readonly record struct CreateFeatureRequestBodyLifecyclePhase : IStringEnum
{
    public static readonly CreateFeatureRequestBodyLifecyclePhase AddOn = new(Values.AddOn);

    public static readonly CreateFeatureRequestBodyLifecyclePhase Alpha = new(Values.Alpha);

    public static readonly CreateFeatureRequestBodyLifecyclePhase Beta = new(Values.Beta);

    public static readonly CreateFeatureRequestBodyLifecyclePhase Deprecated = new(
        Values.Deprecated
    );

    public static readonly CreateFeatureRequestBodyLifecyclePhase Ga = new(Values.Ga);

    public static readonly CreateFeatureRequestBodyLifecyclePhase InPlan = new(Values.InPlan);

    public static readonly CreateFeatureRequestBodyLifecyclePhase Inactive = new(Values.Inactive);

    public static readonly CreateFeatureRequestBodyLifecyclePhase InternalTesting = new(
        Values.InternalTesting
    );

    public static readonly CreateFeatureRequestBodyLifecyclePhase Legacy = new(Values.Legacy);

    public CreateFeatureRequestBodyLifecyclePhase(string value)
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
    public static CreateFeatureRequestBodyLifecyclePhase FromCustom(string value)
    {
        return new CreateFeatureRequestBodyLifecyclePhase(value);
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

    public static bool operator ==(CreateFeatureRequestBodyLifecyclePhase value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateFeatureRequestBodyLifecyclePhase value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateFeatureRequestBodyLifecyclePhase value) =>
        value.Value;

    public static explicit operator CreateFeatureRequestBodyLifecyclePhase(string value) =>
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
