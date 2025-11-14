using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesEngineSchemaVersion>))]
[Serializable]
public readonly record struct RulesEngineSchemaVersion : IStringEnum
{
    public static readonly RulesEngineSchemaVersion Ca729079 = new(Values.Ca729079);

    public static readonly RulesEngineSchemaVersion PlaceholderForFernCompatibility = new(
        Values.PlaceholderForFernCompatibility
    );

    public RulesEngineSchemaVersion(string value)
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
    public static RulesEngineSchemaVersion FromCustom(string value)
    {
        return new RulesEngineSchemaVersion(value);
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

    public static bool operator ==(RulesEngineSchemaVersion value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesEngineSchemaVersion value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesEngineSchemaVersion value) => value.Value;

    public static explicit operator RulesEngineSchemaVersion(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Ca729079 = "ca729079";

        public const string PlaceholderForFernCompatibility = "placeholder-for-fern-compatibility";
    }
}
