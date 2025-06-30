using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateEnvironmentRequestBodyEnvironmentType>))]
[Serializable]
public readonly record struct UpdateEnvironmentRequestBodyEnvironmentType : IStringEnum
{
    public static readonly UpdateEnvironmentRequestBodyEnvironmentType Development = new(
        Values.Development
    );

    public static readonly UpdateEnvironmentRequestBodyEnvironmentType Staging = new(
        Values.Staging
    );

    public static readonly UpdateEnvironmentRequestBodyEnvironmentType Production = new(
        Values.Production
    );

    public UpdateEnvironmentRequestBodyEnvironmentType(string value)
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
    public static UpdateEnvironmentRequestBodyEnvironmentType FromCustom(string value)
    {
        return new UpdateEnvironmentRequestBodyEnvironmentType(value);
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

    public static bool operator ==(
        UpdateEnvironmentRequestBodyEnvironmentType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateEnvironmentRequestBodyEnvironmentType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateEnvironmentRequestBodyEnvironmentType value) =>
        value.Value;

    public static explicit operator UpdateEnvironmentRequestBodyEnvironmentType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Development = "development";

        public const string Staging = "staging";

        public const string Production = "production";
    }
}
