using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<EnvironmentType>))]
[Serializable]
public readonly record struct EnvironmentType : IStringEnum
{
    public static readonly EnvironmentType Development = new(Values.Development);

    public static readonly EnvironmentType Production = new(Values.Production);

    public static readonly EnvironmentType Staging = new(Values.Staging);

    public EnvironmentType(string value)
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
    public static EnvironmentType FromCustom(string value)
    {
        return new EnvironmentType(value);
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

    public static bool operator ==(EnvironmentType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EnvironmentType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EnvironmentType value) => value.Value;

    public static explicit operator EnvironmentType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Development = "development";

        public const string Production = "production";

        public const string Staging = "staging";
    }
}
